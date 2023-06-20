using SantoAndreOnBus.Api.Business.Companies;
using SantoAndreOnBus.Api.Business.Vehicles;

namespace SantoAndreOnBus.Api.Business.Lines;

public record Line
{
    public int Id { get; set; }
    public string? Letter { get; set; }
    public string? Number { get; set; }
    public string? Fromwards { get; set; }
    public string? Towards { get; set; }
    public int PeakHeadway { get; set; }
    public int Departures { get; set; }
    public int? CompanyId { get; set; }
    public virtual Company? Company { get; set; }
    public virtual ICollection<InterestPoint>? InterestPoints { get; set; }
    public virtual ICollection<Place>? Places { get; set; }
    public virtual ICollection<Vehicle>? Vehicles { get; set; }

    public override string ToString()
    {
        return $"{Letter}-{Number}";
    }
}
