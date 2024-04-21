using System.Text.Json.Serialization;
using BusCatalog.Api.Domain.Lines;

namespace BusCatalog.Api.Domain.Places;

public record Place
{
    public int Id { get; set; }
    public required string Identification { get; set; }
    public required string City { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<Line> Lines { get; set; } = [];
}
