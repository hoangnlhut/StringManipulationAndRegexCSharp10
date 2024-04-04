namespace DataProcessing;

internal static class ElectricalEngineeringTotalCalculator
{
    private const string ElectricalEngineeringCode = "ENG001";

    public static decimal CalculateTotalSales(IEnumerable<HistoricalSalesData> data)
    {
        ArgumentNullException.ThrowIfNull(data);
        var totalSales = 0m;

        foreach (var item in data)
        {
            //         eng001   != ENG001 --> false
            //if (item.Category.Code == ElectricalEngineeringCode)
            if (ElectricalEngineeringCode.Equals(item.Category.Code, StringComparison.OrdinalIgnoreCase))
            {
                totalSales += item.Quantity * item.UnitPrice;
            }
        }

        return totalSales;
    }
}
