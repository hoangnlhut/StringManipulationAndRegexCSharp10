using System.Text.RegularExpressions;

namespace DataProcessing;

internal sealed class SalesDataProcessor : Processor<ProcessedSalesData>
{
    private const char SplitChar = '|';
    private readonly CultureInfo _cultureInfo;
    private readonly ILogger<SalesDataProcessor> _logger;

    public SalesDataProcessor(ProcessingOptions processingOptions, CultureInfo cultureInfo) : base(processingOptions)
    {
        _cultureInfo = cultureInfo;
        _logger = processingOptions.LoggerFactory.CreateLogger<SalesDataProcessor>();
    }

    public SalesDataProcessor(ProcessingOptions processingOptions) : this(processingOptions, processingOptions.ApplicationCulture)
    {
    }

    public override async Task<ProcessedSalesData> ProcessAsync(string filename, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(filename);

        var dataReader = new DataReader(Path.Combine(BaseInputPath, filename));
        var processedData = new List<HistoricalSalesData>();
        var failedRows = new List<string>();
        var counter = 1;

        await foreach (var row in dataReader.ReadRowsAsync(cancellationToken))
        {
            var succeeded = false;

            // check malform row
            // we are going to check whether the row we are processing starts with this 
            // unprocessable character. 
            if (row.StartsWith('\u2601'))
            {
                failedRows.Add(row);
                _logger.LogWarning("Row starts with an invalid character");
                continue;
            }

            if (!string.IsNullOrWhiteSpace(row))
            {
                ////var rowParts = row.Split(SplitChar);
                //var rowParts = Regex.Split(row, @"\|");

                //// we use TryParse pattern
                //if (HistoricalSalesData.TryCreateFromHistoricalData(rowParts, _cultureInfo, out HistoricalSalesData? historicalSalesData))
                //{
                //    processedData.Add(historicalSalesData);
                //    succeeded = true;
                //}

                if (HistoricalSalesData.TryCreateFromRow(row, _cultureInfo, out HistoricalSalesData? historicalSalesData))
                {
                    processedData.Add(historicalSalesData);
                    succeeded = true;
                }
            }

            if (!succeeded)
            {
                _logger.LogWarning("Row is invalid and cannot be processed. {FailedRow}.", row);
                failedRows.Add(row);
            }

            _logger.LogInformation("Processing row {RowNumber}: {Result}", counter++, succeeded ? "SUCCEEDED" : "FAILED");
        }

        return new ProcessedSalesData(processedData, failedRows);
    }
}
