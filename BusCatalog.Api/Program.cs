using BusCatalog.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddPersistence();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddModules();
builder.Services.AddControllersWithNamingConvention();
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
