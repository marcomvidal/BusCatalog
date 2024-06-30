using BusCatalog.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));
builder.AddCorsPolicy();
builder.AddAdapters();
builder.Services.AddDomain();
builder.Services.AddControllersWithNamingConvention();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithConfiguration();

var app = builder.Build();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program {}
