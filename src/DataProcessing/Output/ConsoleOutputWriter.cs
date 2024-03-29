namespace DataProcessing;

internal sealed class ConsoleOutputWriter : IOutputWriter
{
    public Task WriteDataAsync(string data, string pathandFileName, CancellationToken cancellationToken = default)
    {
        var beforeColour = Console.ForegroundColor;

        Console.WriteLine();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(data);
        Console.WriteLine();

        Console.ForegroundColor = beforeColour;

        return Task.CompletedTask;
    }
}