using System.Text.Json.Serialization;
using SantoAndreOnBus.Api.Business.Lines;

namespace SantoAndreOnBus.Api.Business.Vehicles;

public record Vehicle
{
    public int Id { get; set; }
    public string Identification { get; set; } = null!;
    public string Description { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Line>? Lines { get; set; }
}
