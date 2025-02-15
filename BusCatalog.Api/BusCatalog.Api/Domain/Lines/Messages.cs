namespace BusCatalog.Api.Domain.Lines;

public static class Messages
{
    public const string GetLinesEndpoint = "Gets all registered lines.";
    public const string GetLineEndpoint = "Gets a line by its identification.";
    public const string PostLineEndpoint = "Registers a new line.";
    public const string PutLineEndpoint = "Updates data of a previously registered line.";
    public const string DeleteLineEndpoint = "Deletes a line by its ID.";
    public const string FetchingAllLines = "Fetching all registered lines.";
    public const string FetchingLineById = "Fetching registered line with ID {id}.";
    public const string FetchingLineByIdentification = "Fetching registered line with identification {identification}.";
    public const string RegisteringLine = "Registering line {identification}.";
    public const string UpdatingLine = "Updated line {identification}.";
    public const string DeletingLine = "Deleted line {identification}.";
}