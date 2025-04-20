namespace BusCatalog.Api.Domain.Lines.Messages;

public static class ServiceMessages
{
    public const string FetchingAllLines = "Fetching all registered lines.";
    public const string FetchingLineById = "Fetching registered line with ID {id}.";
    public const string FetchingLineByIdentification = "Fetching registered line with identification {identification}.";
    public const string RegisteringLine = "Registering line {identification}.";
    public const string UpdatingLine = "Updated line {identification}.";
    public const string DeletingLine = "Deleted line {identification}.";
    public const string LineConsumerStarted = "Line consumer started consuming lines from topic.";
    public const string LineConsuming = "Line is being consumed from the topic: {line}.";
    public const string LineConsumingFailed = "Error occurred while consuming line: {error}";
}