using System.Text.Json.Serialization;
using SantoAndreOnBus.Api.Domain.Lines;

namespace SantoAndreOnBus.Api.Domain.Places;

public record Place
{
    public int Id { get; set; }
    public string Identification { get; set; } = null!;
    public string City { get; set; } = null!;
    
    [JsonIgnore]
    public virtual ICollection<Line> Lines { get; set; } = new List<Line>();
}
