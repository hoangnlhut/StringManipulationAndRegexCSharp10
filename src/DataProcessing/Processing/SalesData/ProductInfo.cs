namespace DataProcessing;

internal readonly struct ProductInfo : IEquatable<ProductInfo>
{
    public const string InvalidValue = "INVALID";
    public static ProductInfo Invalid = new(InvalidValue, InvalidValue);

    public ProductInfo(string saleCode, string sku) => (SalesCode, Sku) = (saleCode, sku);

    public string SalesCode { get; }
    public string Sku { get; }

    public static ProductInfo Parse(string productInfoString)
    {
        ArgumentNullException.ThrowIfNull(productInfoString);
        // TODO - Implementation
        return Invalid;
    }

    public static ProductInfo ParseUsingRegex(string productInfoString)
    {
        ArgumentNullException.ThrowIfNull(productInfoString);
        // TODO - Implementation
        return Invalid;
    }

    public override bool Equals(object? obj) => obj is ProductInfo info && Equals(info);
    public bool Equals(ProductInfo other) => SalesCode == other.SalesCode && Sku == other.Sku;
    public override int GetHashCode() => HashCode.Combine(SalesCode, Sku);
    public static bool operator ==(ProductInfo left, ProductInfo right) => left.Equals(right);
    public static bool operator !=(ProductInfo left, ProductInfo right) => !(left == right);
}
