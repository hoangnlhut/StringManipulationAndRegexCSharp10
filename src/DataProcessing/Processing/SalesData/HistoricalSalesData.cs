using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace DataProcessing;

internal sealed class HistoricalSalesData
{
    private static readonly DateTimeStyles DateStyle = DateTimeStyles.AdjustToUniversal | DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeLocal;

    public string ProductName { get; init; } = string.Empty;
    public long Quantity { get; init; } = -1;
    public string CurrencySymbol { get; init; } = string.Empty;
    public decimal UnitPrice { get; init; } = -1;
    public int SalesTaxPercentage { get; init; } = -1;
    public DateTimeOffset UtcSalesDateTime { get; init; } = DateTimeOffset.MinValue;
    public Category Category { get; init; } = default;
    public ProductInfo ProductInfo { get; init; } = default;
    public string ProductSalesCode => ProductInfo.SalesCode;
    public string ProductSku => ProductInfo.Sku;

    public decimal TotalPrice =>
       UnitPrice < 0 || Quantity < 0
           ? 0
           : UnitPrice == 0 || Quantity == 0 ? 0 : UnitPrice * Quantity;

    public string Currency => CurrencySymbol switch
    {
        "£" => "GBP",
        "$" => "USD",
        "€" => "EUR",
        _ => string.Empty,
    };

    /// <summary>
    /// Factory method used to attempt creation of <see cref="HistoricalSalesData"/>.
    /// </summary>
    public static bool TryCreateFromHistoricalData(string[] sourceData, CultureInfo cultureInfo,
        [NotNullWhen(true)] out HistoricalSalesData? historicalSalesData)
    {
        ArgumentNullException.ThrowIfNull(sourceData);
        ArgumentNullException.ThrowIfNull(cultureInfo);

        historicalSalesData = null;

        if (sourceData.Length == 7)
        {
            var productName = sourceData[0];
            productName = productName.Replace("Color", "Colour");

            if (!long.TryParse(sourceData[1], NumberStyles.Number, cultureInfo, out var quantity))
                return false;

            if (!decimal.TryParse(sourceData[2], NumberStyles.Currency, cultureInfo, out var unitPrice) || unitPrice < 0)
                return false;

            if (!int.TryParse(sourceData[3], NumberStyles.Number, cultureInfo, out var tax) || tax < 0 || tax > 100)
                return false;
            if (!DateTimeOffset.TryParse(sourceData[4], cultureInfo, DateStyle, out var date))
                return false;
            if (!Category.TryParse(sourceData[6], out var category))
                return false;

            var data = new HistoricalSalesData()
            {
                ProductName = productName,
                ProductInfo = ProductInfo.Parse(sourceData[5]),
                Quantity = quantity,
                UnitPrice = unitPrice,
                SalesTaxPercentage = tax,
                UtcSalesDateTime = date,
                Category = category,
                CurrencySymbol = cultureInfo.NumberFormat.CurrencySymbol
            };
            if (data.IsValid)
            {
                historicalSalesData = data;
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Factory method used to attempt creation of <see cref="HistoricalSalesData"/>.
    /// </summary>
    public static bool TryCreateFromRow(string row, CultureInfo cultureInfo,
        [NotNullWhen(true)] out HistoricalSalesData? historicalSalesData)
    {
        ArgumentNullException.ThrowIfNull(row);
        ArgumentNullException.ThrowIfNull(cultureInfo);

        historicalSalesData = null;

        //match all character exept | character, + : it greedily match one or more times. We want at least one character between each pipe 
        //Conclusion: get 6 pieces that every means get at least one character start by all character exept for | and then have | at the end of piece
        // and .+ : any character but one or more character in the end of string
        //after that we add ?: this grouping construct does not capture the substring matched by its subexpression 
        //var match = Regex.Match(row, @"^(?:(?<parts>[^|]+)\|){6}.+$");

        // add other code to process 7th column 
        var match = Regex.Match(row, @"^(?:(?<parts>[^|]+)\|){6}(?<parts>(?<code>[a-zA-Z]{3}\d{3}):(?<desc>.+))$");

        var parts = match.Groups["parts"];

        if (!parts.Success) return false;

        var productName = parts.Captures[0].Value;
        productName = productName.Replace("Color", "Colour");

        if (!long.TryParse(parts.Captures[1].Value, NumberStyles.Number, cultureInfo, out var quantity))
            return false;

        if (!decimal.TryParse(parts.Captures[2].Value, NumberStyles.Currency, cultureInfo, out var unitPrice) || unitPrice < 0)
            return false;

        if (!int.TryParse(parts.Captures[3].Value, NumberStyles.Number, cultureInfo, out var tax) || tax < 0 || tax > 100)
            return false;
        if (!DateTimeOffset.TryParse(parts.Captures[4].Value, cultureInfo, DateStyle, out var date))
            return false;


        var data = new HistoricalSalesData()
        {
            ProductName = productName,
            ProductInfo = ProductInfo.Parse(parts.Captures[5].Value),
            Quantity = quantity,
            UnitPrice = unitPrice,
            SalesTaxPercentage = tax,
            UtcSalesDateTime = date,
            Category = new Category(match.Groups["code"].Value, match.Groups["desc"].Value),
            CurrencySymbol = cultureInfo.NumberFormat.CurrencySymbol
        };
        if (data.IsValid)
        {
            historicalSalesData = data;
            return true;
        }

        return false;
    }

    public static bool TryCreateFromRow(string row, Regex regex, CultureInfo cultureInfo,
       [NotNullWhen(true)] out HistoricalSalesData? historicalSalesData)
    {
        ArgumentNullException.ThrowIfNull(row);
        ArgumentNullException.ThrowIfNull(regex);
        ArgumentNullException.ThrowIfNull(cultureInfo);

        historicalSalesData = null;
        // TODO - Implementation
        return false;
    }

    public bool IsValid =>
        !string.IsNullOrWhiteSpace(ProductName) &&
        Category != default &&
        Quantity >= 0 &&
        UnitPrice >= 0 &&
        SalesTaxPercentage >= 0 && SalesTaxPercentage <= 100 &&
        UtcSalesDateTime > DateTimeOffset.MinValue &&
        !string.IsNullOrWhiteSpace(CurrencySymbol) &&
        !string.IsNullOrWhiteSpace(ProductSalesCode) &&
        !ProductSalesCode.Equals(ProductInfo.InvalidValue) &&
        !string.IsNullOrWhiteSpace(ProductSku) &&
        !ProductSku.Equals(ProductInfo.InvalidValue);

    public string[] GetFormattedComponents(CultureInfo culture)
    {
        return new string[]
        {
            UtcSalesDateTime.ToString("u", culture),
            ProductSalesCode,
            ProductSku,
            ProductName,
            Category.Code,
            Currency,
            UnitPrice.ToString("N2", culture), // format to 2 number after .
            Quantity.ToString("N0", culture), // format to 0 number after .
            SalesTaxPercentage.ToString("N0",culture)
        };
    }

    public void ProduceRow(CultureInfo culture, StringBuilder stringBuilder)
    => _ = stringBuilder
        .AppendFormat(culture, "{0:u}", UtcSalesDateTime)
        .Append(ProductSalesCode).Append(',')
        .Append(ProductSku).Append(",")
        .Append(ProductName).Append(",")
        .Append(Category.Code).Append(",")
        .Append(Currency).Append(",")
        .AppendFormat(culture, "{0:N2}", UnitPrice)
        .AppendFormat(culture, "{0:N0}", Quantity)
        .AppendFormat(culture, "{0:N0}", SalesTaxPercentage)
        .AppendLine();
}
