using Microsoft.EntityFrameworkCore;
using SantoAndreOnBus.Api.Infrastructure;
using AutoMapper;
using SantoAndreOnBus.Api.Domain.General;

namespace SantoAndreOnBus.Api.Domain.Lines;

public interface ILineService
{
    Task<IEnumerable<Line>> GetAllAsync();
    Task<Line?> GetByIdAsync(int id);
    Task<Line?> GetByIdentificationAsync(string identification);
    Task<Line> SaveAsync(LinePostRequest request);
    Task<Line> UpdateAsync(LinePutRequest request, Line line);
    Task<DeleteResponse> DeleteAsync(Line line);
}

public class LineService(
    ILineRepository lineRepository,
    ILineBuilderService lineBuilder,
    IMapper mapper,
    ILogger<LineService> logger) : ILineService
{
    private readonly ILineRepository _lineRepository = lineRepository;
    private readonly ILineBuilderService _lineBuilder = lineBuilder;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<LineService> _logger = logger;

    public async Task<IEnumerable<Line>> GetAllAsync()
    {
        _logger.LogInformation("Fetching all registered lines.");

        return await _lineRepository.GetAllAsync();
    }

    public async Task<Line?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Fetching registered line with ID {id}.", id);

        var line = await _lineRepository.GetByAsync(x => x.Id == id, quantity: 1);

        return line.FirstOrDefault();
    }

    public async Task<Line?> GetByIdentificationAsync(string identification)
    {
        _logger.LogInformation(
            "Fetching registered line with identification {identification}.", identification);

        var line = await _lineRepository.GetByAsync(x => x.Identification == identification);

        return line.FirstOrDefault();
    }

    public async Task<Line> SaveAsync(LinePostRequest request)
    {
        _logger.LogInformation("Registering line {identification}.", request.Identification);

        var line = await _lineBuilder
            .WithLine(_mapper.Map<Line>(request))
            .WithRelantionships(request.Places, request.Vehicles);
        
        await _lineRepository.SaveAsync(line);

        return line;
    }

    public async Task<Line> UpdateAsync(LinePutRequest request, Line line)
    {
        var updatedLine = await _lineBuilder
            .WithLine(_mapper.Map(request, line))
            .WithRelantionships(request.Places, request.Vehicles);
        
        await _lineRepository.UpdateAsync(updatedLine);
        _logger.LogInformation("Updated line {identification}.", request.Identification);

        return updatedLine;
    }

    public async Task<DeleteResponse> DeleteAsync(Line line)
    {
        await _lineRepository.DeleteAsync(line);
        _logger.LogInformation("Deleted line {identification}.", line.Identification);

        return new DeleteResponse(line.Id);
    }
}
