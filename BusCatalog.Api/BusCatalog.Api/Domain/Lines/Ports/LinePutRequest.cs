namespace BusCatalog.Api.Domain.Lines.Ports;

public record LinePutRequest : LinePostRequest
{
    public int Id { get; set; }
}
