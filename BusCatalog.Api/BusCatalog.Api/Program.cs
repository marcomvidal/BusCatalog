using BusCatalog.Api.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddHttpContextAccessor();
builder.AddCorsPolicy();
builder.Services.AddControllersWithNamingConvention();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithConfiguration();
builder.AddAdapters();
builder.AddDomain();

var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program {}
