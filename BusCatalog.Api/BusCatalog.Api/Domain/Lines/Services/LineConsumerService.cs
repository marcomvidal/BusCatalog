using BusCatalog.Api.Domain.Lines.Configurations;
using BusCatalog.Api.Domain.Lines.Ports;
using Confluent.Kafka;
using FluentValidation;
using Microsoft.Extensions.Options;
using static BusCatalog.Api.Domain.Lines.Messages.ServiceMessages;

namespace BusCatalog.Api.Domain.Lines.Services;

public sealed class LineConsumerService : BackgroundService
{
    private readonly IConsumer<Null, LinePostRequest> _consumer;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<LineConsumerService> _logger;
    private readonly LinesConsumerSection _configuration;
    private bool _isReady = false;

    public LineConsumerService(
        IConsumer<Null, LinePostRequest> consumer,
        IServiceScopeFactory serviceScopeFactory,
        IOptions<LinesConsumerSection> configuration,
        ILogger<LineConsumerService> logger,
        IHostApplicationLifetime applicationLifetime)
    {
        _consumer = consumer;
        _serviceScopeFactory = serviceScopeFactory;
        _configuration = configuration.Value;
        _logger = logger;
        applicationLifetime.ApplicationStarted.Register(() => _isReady = true);
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await WaitApplicationStart(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await Task.Run(() => HandleMessage(stoppingToken), stoppingToken);
            }
            catch (Exception exception)
            {
                HandleError(exception);
            }
        }
    }

    private async Task WaitApplicationStart(CancellationToken stoppingToken)
    {
        while (!_isReady)
        {
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }

    private async Task HandleMessage(CancellationToken stoppingToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        _consumer.Subscribe(_configuration.Topic);
        _logger.LogInformation(LineConsumerStarted);
        var message = _consumer.Consume(stoppingToken).Message;
        _logger.LogInformation(LineConsuming, message.Value);

        var validation = await scope.ServiceProvider
            .GetRequiredService<IValidator<LinePostRequest>>()
            .ValidateAsync(message.Value, stoppingToken);

        if (!validation.IsValid)
        {
            throw new ValidationException(validation.Errors);
        }

        await scope.ServiceProvider
            .GetRequiredService<ILineService>()
            .SaveAsync(message.Value);
    }

    private void HandleError(Exception exception)
    {
        _logger.LogError(exception, LineConsumingFailed, exception.Message);
    }
}