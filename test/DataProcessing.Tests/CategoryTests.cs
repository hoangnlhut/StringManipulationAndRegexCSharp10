using System.Collections.Generic;

using Xunit;

namespace DataProcessing.Tests;

public class CategoryTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void TryParse(string input, string? code, string? description, bool shouldBeParsed)
    {
        if (shouldBeParsed)
        {
            Assert.True(Category.TryParse(input, out var category));
            Assert.Equal(code, category.Code);
            Assert.Equal(description, category.Description);
        }
        else
        {
            Assert.False(Category.TryParse(input, out _));
        }
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void TryParseUsingRegex(string input, string? code, string? description, bool shouldBeParsed)
    {
        if (shouldBeParsed)
        {
            Assert.True(Category.TryParseUsingRegex(input, out var category));
            Assert.Equal(code, category.Code);
            Assert.Equal(description, category.Description);
        }
        else
        {
            Assert.False(Category.TryParseUsingRegex(input, out _));
        }
    }

    public static IEnumerable<object?[]> TestData =>
        new List<object?[]>
        {
            new object?[] { null, null, null, false },
            new object?[] { "", null, null, false },
            new object?[] { " ", null, null, false },
            new object?[] { "ENG001:Engineering", "ENG001", "Engineering", true },
            new object?[] { "ENG001:Engineering (Mechanical)", "ENG001", "Engineering (Mechanical)", true },
            new object?[] { "   ENG001  :   Engineering ", "ENG001", "Engineering", true },
            new object?[] { "EG001:Engineering", null, null, false },
            new object?[] { "ENG001:", null, null, false },
            new object?[] { " ENG001", null, null, false }
        };
}
