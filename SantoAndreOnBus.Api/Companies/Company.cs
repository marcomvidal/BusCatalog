using SantoAndreOnBus.Api.Lines;
using System.Text.Json.Serialization;

namespace SantoAndreOnBus.Api.Companies;

public record Company
{
    public int Id { get; set; }
    public string? Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<Line>? Lines { get; set; }

    public virtual ICollection<Prefix> Prefixes { get; set;} = null!;
}
