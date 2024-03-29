namespace DataProcessing.Benchmarks;

public static class SampleData
{
    // 10 rows of data
    public static readonly string[] Rows = new string[]
    {
        "Widget 87|2|3|20|17/02/1998 3:45PM|13554632:AB65|A1:", // INAVLID
        "Widget 87|2|3|20|17/02/1998 3:45PM|13554632:AB65|A1:Invalid", // INAVLID
        "Widget 87|2|3|20|17/02/1998 3:45PM|13554632:AB65", // INAVLID
        "Widget 87|2|3|20|17/02/1998 3:45PM|13554632:AB65|AAA001:", // INAVLID
        "Widget 87|2|3|20|17/02/1998 3:45PM|13554632:AB65|Invalid", // INAVLID
        "Widget 87|2|3|20|17/02/1998 3:45PM|13554632:AB65|AAA001:Valid",
        "Widget 87|2|3|20|17/02/1998 3:45PM|13554632:AB65|AAA001:Valid",
        "Widget 87|2|3|20|17/02/1998 3:45PM|13554632:AB65|AAA001:Valid",
        "Widget 87|2|3|20|17/02/1998 3:45PM|13554632:AB65|AAA001:Valid",
        "Widget 87|2|3|20|17/02/1998 3:45PM|13554632:AB65|AAA001:Valid"
    };
}
