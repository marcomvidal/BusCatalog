using SantoAndreOnBus.Api.Business.Lines;
using System.Text.Json.Serialization;

namespace SantoAndreOnBus.Api.Business.Companies;

public record Company
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Line>? Lines { get; set; }

    public virtual ICollection<Prefix> Prefixes { get; set;} = null!;
}
