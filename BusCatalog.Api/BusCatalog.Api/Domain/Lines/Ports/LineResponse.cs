namespace BusCatalog.Api.Domain.Lines.Ports;

public record LineResponse
{
    public int Id { get; set; }
    public required string Identification { get; set; }
    public required string Fromwards { get; set; }
    public required string Towards { get; set; }
    public int DeparturesPerDay { get; set; }
    public IEnumerable<string> Vehicles { get; set; } = [];
}
