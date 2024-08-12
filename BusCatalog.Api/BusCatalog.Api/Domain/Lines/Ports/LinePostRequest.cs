namespace BusCatalog.Api.Domain.Lines.Ports;

public record LinePostRequest
{
    private IEnumerable<string> _vehicles = [];
    public string? Identification { get; set; }
    public string? Fromwards { get; set; }
    public string? Towards { get; set; }
    public int? DeparturesPerDay { get; set; }

    public IEnumerable<string> Vehicles
    {
        get => _vehicles.Select(x => x.UpperSnakeCasefy());
        set => _vehicles = value;
    }
}
