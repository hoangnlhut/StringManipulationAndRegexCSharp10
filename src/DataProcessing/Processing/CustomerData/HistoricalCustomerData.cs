namespace DataProcessing;

internal sealed class HistoricalCustomerData
{
    public HistoricalCustomerData(Guid id, string code, string country)
    {
        ArgumentNullException.ThrowIfNull(code);
        ArgumentNullException.ThrowIfNull(country);

        Id = id;
        CustomerCode = code;
        Country = country;
    }

    public Guid Id { get; }
    public string CustomerCode { get; }
    public string Country { get; }
}
