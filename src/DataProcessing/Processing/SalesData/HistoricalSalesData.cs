using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DataProcessing;

internal sealed class HistoricalSalesData
{
    public string ProductName { get; init; }
    public long Quantity { get; init; } = -1;
    public string CurrencySymbol { get; init; }
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
        // TODO - Implementation
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
