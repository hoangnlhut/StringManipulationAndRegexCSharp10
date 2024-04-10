using System.Text.RegularExpressions;

namespace DataProcessing;

internal readonly struct ProductInfo : IEquatable<ProductInfo>
{
    //language=regex
    private const string ParsePattern = @"^(?<code>\d+)[:-](?:[a-z]#)?(?!.*#|.*-)(?<sku>[^(]+)";

    //language=regex
    public const string ProductInfoParse = @"(?<parts>.*?)\-(?<parts>.*)";
    public const string InvalidValue = "INVALID";
    public static ProductInfo Invalid = new(InvalidValue, InvalidValue);

    public ProductInfo(string saleCode, string sku) => (SalesCode, Sku) = (saleCode, sku);

    public string SalesCode { get; }
    public string Sku { get; }

    public static ProductInfo Parse(string productInfoString)
    {
        ArgumentNullException.ThrowIfNull(productInfoString);

        //replace will avoid the need to potentially allocate a new string by using the replace method call.
        //productInfoString = productInfoString.Replace(':', '-');

        if (string.Empty.Equals(productInfoString) || (!productInfoString.Contains('-') && !productInfoString.Contains(':')))
        {
            return Invalid;
        }

        var parts = productInfoString.Contains('-') ? productInfoString.Split('-'): productInfoString.Split(':');

        if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
        {
            return Invalid;
        }

        var salesCode = parts[0];
        var sku = parts[1];

        if (salesCode.Any(c => !char.IsDigit(c)))
        {
            return Invalid;
        }

        if (char.IsLower(sku[0]) && sku[1].Equals('#'))
        {
            sku = sku[2..];
        }
        else if (sku.Contains('#'))
        {
            return Invalid;
        }

        var parenStart = sku.IndexOf('(');
        if (parenStart != -1)
        {
            sku = sku.Remove(parenStart);
        }

        return new ProductInfo(salesCode, sku);
    }

    public static ProductInfo ParseUsingRegex(string productInfoString)
    {
        ArgumentNullException.ThrowIfNull(productInfoString);

        var match = Regex.Match(productInfoString, ParsePattern, RegexOptions.None, TimeSpan.FromSeconds(1));

        return match.Success && match.Groups.ContainsKey("code") && match.Groups.ContainsKey("sku") ?
          new ProductInfo(match.Groups["code"].Value, match.Groups["sku"].Value)  : Invalid;
    }

    public override bool Equals(object? obj) => obj is ProductInfo info && Equals(info);
    public bool Equals(ProductInfo other) => SalesCode == other.SalesCode && Sku == other.Sku;
    public override int GetHashCode() => HashCode.Combine(SalesCode, Sku);
    public static bool operator ==(ProductInfo left, ProductInfo right) => left.Equals(right);
    public static bool operator !=(ProductInfo left, ProductInfo right) => !(left == right);
}
