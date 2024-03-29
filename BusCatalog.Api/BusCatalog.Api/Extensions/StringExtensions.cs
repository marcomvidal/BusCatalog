using System.Text.RegularExpressions;

namespace BusCatalog.Api.Extensions;

public static partial class StringExtensions
{
    public static string SlugfyUpper(this string text) =>
        SlugfyRegex()
            .Replace(text, "-")
            .ToUpper();
    
    [GeneratedRegex(@"[\s_]")]
    private static partial Regex SlugfyRegex();
}