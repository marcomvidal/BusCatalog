using System.Text.RegularExpressions;

namespace BusCatalog.Api.Extensions;

public static partial class StringExtensions
{
    public static string UpperSlugfy(this string text) =>
        SlugfyRegex().Replace(text, "-").ToUpper();

    public static string UpperSnakeCasefy(this string text) =>
        SlugfyRegex().Replace(text, "_").ToUpper();
    
    [GeneratedRegex(@"[\s_]")]
    private static partial Regex SlugfyRegex();
}