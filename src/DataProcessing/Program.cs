global using System.Globalization;
global using DataProcessing;
global using DataProcessing.Input;
global using DataProcessing.Reporting;
global using Microsoft.Extensions.Logging;

// Marks when the app is past the user data collection phase.
var initialized = false; 

using var cts = new CancellationTokenSource();

// Register shutdown event handler.
Console.CancelKeyPress += (s, e) =>
{
    Console.WriteLine();
    var beforeColour = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Canceling...");
    Console.ForegroundColor = beforeColour;
    Console.WriteLine();
    cts.Cancel();

    if (initialized)
        e.Cancel = false;
};

try
{
    // Configure a logger factory that can provide loggers where required for logging progress.
    using var loggerFactory = LoggerFactory.Create(builder => builder
        .AddFilter("Microsoft", LogLevel.Warning)
        .AddFilter("System", LogLevel.Warning)
        .AddFilter("DataProcessing", LogLevel.Information)
        .AddConsole());

    // Set the output to Unicode to ensure we can display emojis consistently.
    Console.OutputEncoding = System.Text.Encoding.Unicode;

     // Configure the options for the processor.
    // Adds two output writers used when reports are generated.
    var options = new ProcessingOptions(loggerFactory)
        .AddOutputWriter(new ThirdPartyOutputWriter())
        .AddOutputWriter(new ConsoleOutputWriter());

    // Mark app as initialized
    initialized = true;

    // Begin processing.
    await DataProcessor.ProcessAsync(options, cts.Token);

    // Allow enough time for logs to 'flush' to console before completing.
    await Task.Delay(250);

    Console.WriteLine();
    Console.WriteLine("COMPLETED: Press any key to exit.");
    Console.ReadKey();
}
catch (OperationCanceledException)
{
    Console.WriteLine();
    Console.WriteLine("CANCELLED: Press any key to exit.");
    Console.ReadKey();
}