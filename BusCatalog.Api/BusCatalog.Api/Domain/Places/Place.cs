using System.Text.Json.Serialization;
using BusCatalog.Api.Domain.Lines;

namespace BusCatalog.Api.Domain.Places;

public record Place
{
    public int Id { get; set; }
    public string Identification { get; set; } = null!;
    public string City { get; set; } = null!;
    
    [JsonIgnore]
    public virtual ICollection<Line> Lines { get; set; } = [];
}
