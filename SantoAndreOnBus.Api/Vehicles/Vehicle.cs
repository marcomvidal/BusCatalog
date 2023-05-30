using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using SantoAndreOnBus.Api.Lines;

namespace SantoAndreOnBus.Api.Vehicles;

public record Vehicle
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(4, ErrorMessage = "O nome deve conter, no mínimo, 4 caracteres")]
    [MaxLength(30, ErrorMessage = "O nome deve conter, no máximo, 30 caracteres")]
    public string? Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<LineVehicle>? LineVehicles { get; set; }
}
