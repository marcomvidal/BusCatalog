using FluentValidation;

namespace SantoAndreOnBus.Api.Domain.Places;

public static class Configuration
{
    public static IServiceCollection AddPlaces(this IServiceCollection services) =>
        services
            .AddScoped<IPlaceRepository, PlaceRepository>()
            .AddScoped<IPlaceService, PlaceService>()
            .AddScoped<IValidator<PlacePostRequest>, PlacePostValidator>()
            .AddScoped<IValidator<PlacePutRequest>, PlacePutValidator>()
            .AddScoped<IPlaceValidator, PlaceValidator>();
}