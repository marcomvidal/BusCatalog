namespace BusCatalog.Api.Domain.Lines.Ports;

public sealed record LinePutRequest : LinePostRequest
{
    public int Id { get; set; }
}
