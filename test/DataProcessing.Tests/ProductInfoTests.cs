using System.Collections.Generic;
using Xunit;

namespace DataProcessing.Tests;

public class ProductInfoTests
{
    private const string Invalid = ProductInfo.InvalidValue;

    [Theory]
    [MemberData(nameof(TestData))]
    public void Parse_ReturnsExpectedProductInfo(string productCode, string expectedProductSalesCode, string expectedProductSku)
    {
        var productInfo = ProductInfo.Parse(productCode);

        Assert.Equal(expectedProductSalesCode, productInfo.SalesCode);
        Assert.Equal(expectedProductSku, productInfo.Sku);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void ParseUsingRegex_ReturnsExpectedProductInfo(string productCode, string expectedProductSalesCode, string expectedProductSku)
    {
        var productInfo = ProductInfo.ParseUsingRegex(productCode);

        Assert.Equal(expectedProductSalesCode, productInfo.SalesCode);
        Assert.Equal(expectedProductSku, productInfo.Sku);
    }

    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { "123-SKU", "123", "SKU" },
            new object[] { "", Invalid, Invalid },
            new object[] { "INVALIDCODE_SKU", Invalid, Invalid },
            new object[] { "123-", Invalid, Invalid },
            new object[] { "-SKU", Invalid, Invalid },
            new object[] { "123-SKU-INVALID", Invalid, Invalid },
        };
}
