using System.Text.RegularExpressions;

namespace DataProcessing;

internal sealed class CustomerDataProcessor : Processor<ProcessedCustomerData>
{
    private const string RegularCustomerStartCode = "BA";

    //language=regex
    private const string CustomerDataPattern = @"\[(?<data>.*?)\]";

    //using a static field here ensure that we will reuse the same instance of
    // our compiled Regex accross all instances of the CustomerDataProcessor.
    private static Regex CustomerRegex = new Regex(CustomerDataPattern, RegexOptions.Compiled, TimeSpan.FromSeconds(1));

    public CustomerDataProcessor(ProcessingOptions processingOptions) : base(processingOptions)
    {
    }

    public override async Task<ProcessedCustomerData> ProcessAsync(string filename, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filename);

        var dataReader = new DataReader(Path.Combine(BaseInputPath, filename));

        var priorityCustomers = new List<HistoricalCustomerData>();
        var regularCustomers = new List<HistoricalCustomerData>();

        await foreach (var row in dataReader.ReadRowsAsync(cancellationToken))
        {
            //var matches = Regex.Matches(row, CustomerDataPattern, RegexOptions.None, TimeSpan.FromSeconds(1)) old;
            var matches = CustomerRegex.Matches(row); //optimze regex code

            if (matches.Count == 4)
            {
                var customerCode = matches[0].Groups["data"].Value;

                if (!Guid.TryParseExact(matches[1].Groups["data"].Value, "D", out var parsedGuid ))
                continue;

                var country = matches[2].Groups["data"].Value;

                var data  = new HistoricalCustomerData(parsedGuid, country, country);

                //since we don't require culture-aware comparison so we will use non-sensitive 
                var compareResult = string.CompareOrdinal(customerCode, RegularCustomerStartCode);
                if (compareResult < 0)
                {
                    priorityCustomers.Add(data);
                }
                else
                {
                    regularCustomers.Add(data);
                }
                
            }
         }

        return new ProcessedCustomerData(priorityCustomers, regularCustomers);
    }
}
