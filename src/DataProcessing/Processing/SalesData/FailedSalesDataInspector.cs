namespace DataProcessing;

internal class FailedSalesDataInspector
{
    private readonly ILogger _logger;

    public FailedSalesDataInspector(ProcessingOptions options) => _logger = options.LoggerFactory.CreateLogger<FailedSalesDataInspector>();

    public void InspectAll(IEnumerable<string> failedRows)
    {
        foreach (var failedRow in failedRows)
            Inspect(failedRow);
    }

    public void Inspect(string failedRow)
    {
        ArgumentNullException.ThrowIfNull(failedRow);

        //var separatorCount = failedRow.Count(c => c.Equals('|'));
        var separatorCount = 0;
        for (var i = 0; i < failedRow.Length; i++)
        {
            if (failedRow[i].Equals('|'))
            {
                separatorCount++;
            }
        }

        if (separatorCount < 6)
        {
            _logger.LogWarning($"{failedRow} has too few elements");
            return;
        }

        if (separatorCount > 6)
        {
            _logger.LogWarning($"{failedRow} has too many elements");
            return;
        }

        var lastSeparatorIndex = failedRow.LastIndexOf('|');
        var categoryColonIndex = failedRow.IndexOf(':', lastSeparatorIndex);

        if (categoryColonIndex == -1)
        {
            _logger.LogWarning($"{failedRow} category element is invalid due to missing colon");
            return;
        }

        if (categoryColonIndex == failedRow.Length - 1)
        {
            _logger.LogWarning($"{failedRow} category code was expected to be followed by a description");
            return;
        }

        var codeLength = 0;
        for (var index = lastSeparatorIndex + 1;  index < categoryColonIndex; index++)
        {
            if (!char.IsWhiteSpace(failedRow[index]))
            {
                codeLength++;
            }
        }

        if (codeLength != 6)
        {
            _logger.LogWarning($"{failedRow} category code has invalid length {codeLength}");
            return;
        }

        var hasDescription = false;
        for (var index = categoryColonIndex + 1; index < failedRow.Length; index++)
        {
            if (!char.IsWhiteSpace(failedRow[index]))
            {
                hasDescription = true;
                break;
            }
        }

        if (!hasDescription) 
        {
            _logger.LogWarning($"{failedRow} category description contained only whitespace");
            return;
        }
    }
}