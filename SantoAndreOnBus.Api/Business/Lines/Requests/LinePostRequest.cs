namespace SantoAndreOnBus.Api.Business.Lines;

public record LinePostRequest
{
    public string Identification { get; set; } = null!;
    public string Fromwards { get; set; } = null!;
    public string Towards { get; set; } = null!;
    public int DeparturesPerDay { get; set; }
    public IEnumerable<int> PlacesIdList { get; set; } = new List<int>();
    public IEnumerable<string> VehiclesIdentifications { get; set; } = new List<string>();
}
