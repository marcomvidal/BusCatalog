namespace SantoAndreOnBus.Api.Business.Lines;

public record LinePutRequest : LinePostRequest
{
    public int Id { get; set; }
}
