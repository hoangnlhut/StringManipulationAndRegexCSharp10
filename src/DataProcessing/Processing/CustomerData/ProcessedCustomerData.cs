namespace DataProcessing;

internal sealed class ProcessedCustomerData
{
    public ProcessedCustomerData(List<HistoricalCustomerData> priorityCustomers, List<HistoricalCustomerData> customers)
    {
        ArgumentNullException.ThrowIfNull(priorityCustomers);
        ArgumentNullException.ThrowIfNull(customers);

        PriorityCustomers = priorityCustomers;
        RegularCustomers = customers;
    }

    public IReadOnlyCollection<HistoricalCustomerData> PriorityCustomers { get; }
    public IReadOnlyCollection<HistoricalCustomerData> RegularCustomers { get; }
}