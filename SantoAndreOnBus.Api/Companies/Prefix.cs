using SantoAndreOnBus.Api.Lines;
using System.Text.Json.Serialization;

namespace SantoAndreOnBus.Api.Companies;

public record Prefix
{
    public int Id { get; set; }
    public string? Identification { get; set; }
    public int CompanyId { get; set; }

    [JsonIgnore]
    public virtual Company? Company { get; set; }

    [JsonIgnore]
    public virtual ICollection<Line>? Lines { get; set; }
}
