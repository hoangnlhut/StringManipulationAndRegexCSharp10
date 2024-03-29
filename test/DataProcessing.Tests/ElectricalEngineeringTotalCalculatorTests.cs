using System.Collections.Generic;
using Xunit;

namespace DataProcessing.Tests;

public class ElectricalEngineeringTotalCalculatorTests
{
    [Fact]
    public void CalculatesExpectedTotal()
    {
        var total = ElectricalEngineeringTotalCalculator.CalculateTotalSales(SampleData);
        Assert.Equal(40, total);
    }

    private static readonly IEnumerable<HistoricalSalesData> SampleData = 
        new List<HistoricalSalesData>
    {
        new HistoricalSalesData { Category = new Category("ENG001", "Description"), 
            UnitPrice = 10, Quantity = 1 },
        new HistoricalSalesData { Category = new Category("eng001", "Description"), 
            UnitPrice = 10, Quantity = 1 },
        new HistoricalSalesData { Category = new Category("ENG002", "Description"),
            UnitPrice = 10, Quantity = 1 },
        new HistoricalSalesData { Category = new Category("eng002", "Description"), 
            UnitPrice = 10, Quantity = 1 },
        new HistoricalSalesData { Category = new Category("SAL000", "Description"), 
            UnitPrice = 10, Quantity = 1 },
        new HistoricalSalesData { Category = new Category("ENG001", "Description"), 
            UnitPrice = 10, Quantity = 2 }
    };
}
