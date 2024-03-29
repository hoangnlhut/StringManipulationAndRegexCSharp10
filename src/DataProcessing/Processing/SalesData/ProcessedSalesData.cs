namespace DataProcessing;

internal readonly struct ProcessedSalesData : IEquatable<ProcessedSalesData>
{
    public ProcessedSalesData(List<HistoricalSalesData> processedSalesData, List<string> failedRows)
    {
        ArgumentNullException.ThrowIfNull(processedSalesData);
        ArgumentNullException.ThrowIfNull(failedRows);

        ProcessedData = processedSalesData.AsReadOnly();
        FailedRows = failedRows.AsReadOnly();
    }

    public IReadOnlyCollection<HistoricalSalesData> ProcessedData { get; }
    public IReadOnlyCollection<string> FailedRows { get; }
    public bool HasProcessedData => ProcessedData.Any();
    public bool HasFailedRows => FailedRows.Any();

    public bool Equals(ProcessedSalesData other) => 
        ProcessedData.Equals(other.ProcessedData) && 
        FailedRows.Equals(other.FailedRows);

    public override bool Equals(object? obj) => 
        obj is not null && 
        obj is ProcessedSalesData other && Equals(other); 

    public static bool operator ==(ProcessedSalesData a, ProcessedSalesData b) => a.Equals(b);
    public static bool operator !=(ProcessedSalesData a, ProcessedSalesData b) => !(a == b);
    public override int GetHashCode() => (ProcessedData, FailedRows).GetHashCode();
    public override string? ToString() => ProcessedData.ToString();
}
