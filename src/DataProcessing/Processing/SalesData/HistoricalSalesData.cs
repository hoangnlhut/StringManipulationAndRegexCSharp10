using System.Diagnostics.CodeAnalysis;
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
        
        if(sourceData.Length == 7)
        {
            var productName = sourceData[0];
            if (!long.TryParse(sourceData[1], NumberStyles.Number, cultureInfo , out var quantity))
                return false;

            if (!decimal.TryParse(sourceData[2], NumberStyles.Currency, cultureInfo, out var unitPrice) || unitPrice < 0)
                return false;

            if (!int.TryParse(sourceData[3], NumberStyles.Number, cultureInfo, out var tax) || tax < 0 || tax > 100)
                return false;
            if(!DateTimeOffset.TryParse(sourceData[4], cultureInfo, DateStyle, out var date)) 
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
        // TODO - Implementation
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

    public bool IsValid => true;
}
