using SantoAndreOnBus.Api.Business.Places;
using SantoAndreOnBus.Api.Business.Vehicles;

namespace SantoAndreOnBus.Api.Business.Lines;

public record Line
{
    public int Id { get; set; }
    public string Identification { get; set; } = null!;
    public string? Fromwards { get; set; } = null!;
    public string? Towards { get; set; } = null!;
    public int DeparturesPerDay { get; set; }
    public virtual ICollection<Place>? Places { get; set; } = new List<Place>();
    public virtual ICollection<Vehicle>? Vehicles { get; set; } = new List<Vehicle>();
}
