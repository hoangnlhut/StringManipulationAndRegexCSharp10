namespace DataProcessing;

internal sealed class SessionContext
{
    public SessionContext(string forename, string surname, string departmentCode)
    {
        Forename = forename;
        Surname = surname;
        DepartmentCode = departmentCode;
    }

    public string Forename { get; }

    public string Surname { get; }

    public string DepartmentCode { get; }
}
