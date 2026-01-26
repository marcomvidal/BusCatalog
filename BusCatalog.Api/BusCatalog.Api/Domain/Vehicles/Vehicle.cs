using System.Text.Json.Serialization;
using BusCatalog.Api.Domain.Lines;

namespace BusCatalog.Api.Domain.Vehicles;

public record Vehicle
{
    private string _identification = null!;

    public int Id { get; set; }

    public required string Identification
    {
        get => _identification.UpperSnakeCasefy();
        set => _identification = value;
    }

    public required string Description { get; set; }

    [JsonIgnore]
    public virtual ICollection<Line>? Lines { get; set; }
}
