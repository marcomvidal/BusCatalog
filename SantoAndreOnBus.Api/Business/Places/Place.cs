using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using SantoAndreOnBus.Api.Business.Lines;

namespace SantoAndreOnBus.Api.Business.Places;

public record Place
{
    public int Id { get; set; }

    [Required(ErrorMessage = "A identificação é obrigatória")]
    [MinLength(4, ErrorMessage = "A identificação deve conter, no mínimo, 4 caracteres")]
    [MaxLength(50, ErrorMessage = "A identificação deve conter, no máximo, 50 caracteres")]
    public string Identification { get; set; } = null!;

    [Required(ErrorMessage = "A cidade é obrigatória")]
    [MinLength(4, ErrorMessage = "A cidade deve conter, no mínimo, 4 caracteres")]
    [MaxLength(50, ErrorMessage = "A cidade deve conter, no máximo, 50 caracteres")]
    public string City { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Line>? Lines { get; set; }
}
