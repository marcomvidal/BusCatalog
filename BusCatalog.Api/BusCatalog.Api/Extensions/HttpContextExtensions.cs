namespace BusCatalog.Api.Extensions;

public static class HttpContextExtensions
{
    public static string GetFullRequestUrl(this HttpContext context)
    {
        return $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
    }
}