using System.Text.RegularExpressions;

namespace BusCatalog.Api.Extensions;

public static class StringExtensions
{
    extension(string text)
    {
        public string UpperSlugfy() =>
            new Regex(@"[\s_]").Replace(text, "-").ToUpper();

        public string UpperSnakeCasefy() =>
            new Regex(@"[\s_]").Replace(text, "_").ToUpper(); 
    }
}