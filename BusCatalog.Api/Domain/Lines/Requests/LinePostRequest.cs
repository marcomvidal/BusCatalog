using BusCatalog.Api.Extensions;

namespace BusCatalog.Api.Domain.Lines;

public record LinePostRequest
{
    private IEnumerable<string> _vehicles = new List<string>();
    public string Identification { get; set; } = string.Empty;
    public string Fromwards { get; set; } = string.Empty;
    public string Towards { get; set; } = string.Empty;
    public int DeparturesPerDay { get; set; }

    public IEnumerable<string> Vehicles
    {
        get => _vehicles.Select(x => x.SlugfyUpper());
        set => _vehicles = value;
    }
    
    public IEnumerable<int> Places { get; set; } = new List<int>();
}
