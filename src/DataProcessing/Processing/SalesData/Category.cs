using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DataProcessing;

internal readonly struct Category : IEquatable<Category>
{
    //language=regex
    private const string CategoryPattern = @"^\s*([a-zA-Z]{3}[0-3]{3})\s*:\s*(.+?)\s*$";

    public static Category Empty = new(string.Empty, string.Empty);

    public Category(string code, string description) => (Code, Description) = (code, description);

    public string Code { get; }

    public string Description { get; }

    public static bool TryParse(string source, [MaybeNullWhen(false)] out Category category)
    {
        category = Empty;
        #region Hoang do himself
        //if (string.IsNullOrWhiteSpace(source) || !source.Contains(':') || source.Length < 8) return false;

        //var parts = source.Split(':');
        //if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
        //{
        //    return false;
        //}

        //category = new Category(parts[0].Trim(), parts[1].Trim());
        //return true;
        #endregion
        if (string.IsNullOrWhiteSpace(source) || source.Length < 8) return false;
       
        var colonIndex = source.IndexOf(':');
        source = source.Trim();
        //found
        if (colonIndex > -1 )
        {
            //var categoryCode = source.Substring(0, 6);
            var categoryCode = source[..6]; // use range operator

            for ( int index = 0; index < categoryCode.Length; index++ )
            {
                if (index < 3 && !char.IsLetter(source, index))
                {
                    return false;
                }

                var character = categoryCode[index];
                if(index >= 3 && character != '0' && character != '1' && character != '2' && character != '3')
                {
                    return false;
                }
            }

            var categoryDescription = source.Substring(colonIndex + 1).TrimStart();
            category = new Category(categoryCode.ToUpperInvariant(), categoryDescription);
            return true;
        }

        return false;
    }

    public static bool TryParseUsingRegex(string source, [MaybeNullWhen(false)] out Category category)
    {
        category = Empty;
        if (string.IsNullOrEmpty(source)) return false;

        var match = Regex.Match(source, CategoryPattern, RegexOptions.None, TimeSpan.FromSeconds(1));
        if (match.Success && match.Groups.Count == 3)
        {
            var categoryCode = match.Groups[1].Value.ToUpperInvariant();
            var description = match.Groups[2].Value;

            category = new Category(categoryCode, description);
            return true;
        }
        return false;
    }

    public override bool Equals(object? obj) => obj is Category category && Equals(category);

    public bool Equals(Category other) => Code == other.Code && Description == other.Description;

    public override int GetHashCode() => HashCode.Combine(Code, Description);

    public static bool operator ==(Category left, Category right) => left.Equals(right);

    public static bool operator !=(Category left, Category right) => !(left == right);
}
