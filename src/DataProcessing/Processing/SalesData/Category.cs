using System.Diagnostics.CodeAnalysis;

namespace DataProcessing;

internal readonly struct Category : IEquatable<Category>
{
    public static Category Empty = new(string.Empty, string.Empty);

    public Category(string code, string description) => (Code, Description) = (code, description);

    public string Code { get; }

    public string Description { get; }

    public static bool TryParse(string source, [MaybeNullWhen(false)] out Category category)
    {
        category = Empty;
        return true;
    }

    public static bool TryParseUsingRegex(string source, [MaybeNullWhen(false)] out Category category)
    {
        category = Empty;
        return true;
    }

    public override bool Equals(object? obj) => obj is Category category && Equals(category);

    public bool Equals(Category other) => Code == other.Code && Description == other.Description;

    public override int GetHashCode() => HashCode.Combine(Code, Description);

    public static bool operator ==(Category left, Category right) => left.Equals(right);

    public static bool operator !=(Category left, Category right) => !(left == right);
}
