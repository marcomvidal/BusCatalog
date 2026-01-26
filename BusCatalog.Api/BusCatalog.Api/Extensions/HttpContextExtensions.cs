namespace BusCatalog.Api.Extensions;

public static class HttpContextExtensions
{
    extension(HttpContext context)
    {
        public string FullRequestUrl
        {
            get => $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}";
        }
    }
}