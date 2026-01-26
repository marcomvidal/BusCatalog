using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace BusCatalog.Api.Infrastructure.Configurations;

public static class RoutingConvention
{
    public static IMvcBuilder AddControllersWithNamingConvention(
        this IServiceCollection services) =>
        services.AddControllers(options => 
            options.Conventions.Add(
                new RouteTokenTransformerConvention(
                    new SlugifyParameterTransformer())));
}

public sealed partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex SlugfyRegex();

    public string? TransformOutbound(object? value) =>
        value is not null && !string.IsNullOrEmpty(value.ToString())
            ? SlugfyRegex().Replace(value.ToString()!, "$1-$2").ToLower()
            : null;
}
