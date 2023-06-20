using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using SantoAndreOnBus.Api.Business.Lines;

namespace SantoAndreOnBus.Api.Business.Vehicles;

public record Vehicle
{
    public int Id { get; set; }

    [Required(ErrorMessage = "A identificação é obrigatória")]
    [MinLength(4, ErrorMessage = "A identificação deve conter, no mínimo, 4 caracteres")]
    [MaxLength(30, ErrorMessage = "A identificação deve conter, no máximo, 30 caracteres")]
    public string Identification { get; set; } = null!;

    public string Description { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Line>? Lines { get; set; }
}
