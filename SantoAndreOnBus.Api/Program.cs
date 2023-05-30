using SantoAndreOnBus.Api.Infrastructure.Configurations;
using SantoAndreOnBus.Api.Infrastructure.Dependencies;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Consolidate();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddPersistence(configuration.ConnectionString!);
builder.Services.AddRepositories();
builder.Services.AddJwtAuthentication(configuration.Authentication!);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program {}
