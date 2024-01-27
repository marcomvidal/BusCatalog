using FluentAssertions;
using Xunit;

namespace BusCatalog.Test.Cases.Extensions;

public class String
{
    [Theory]
    [InlineData("", "")]
    [InlineData("articulated", "ARTICULATED")]
    [InlineData("super articulated", "SUPER-ARTICULATED")]
    [InlineData("super duper articulated", "SUPER-DUPER-ARTICULATED")]
    public void WhenSlugfiesString_ShouldSuccess(string input, string result) =>
        input.SlugfyUpper().Should().Be(result);
}
