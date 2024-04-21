namespace BusCatalog.Api.Domain.Lines;

public record LinePutRequest : LinePostRequest
{
    public int Id { get; set; }
}
