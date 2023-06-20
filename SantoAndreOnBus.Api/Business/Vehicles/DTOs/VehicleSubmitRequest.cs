using System.ComponentModel.DataAnnotations;

namespace SantoAndreOnBus.Api.Business.Vehicles;

public record VehicleSubmitRequest
{
    [Required(ErrorMessage = "A identificação é obrigatória")]
    [MinLength(4, ErrorMessage = "A identificação deve conter, no mínimo, 4 caracteres")]
    [MaxLength(30, ErrorMessage = "A identificação deve conter, no máximo, 30 caracteres")]
    public string Identification { get; set; } = null!;

    [Required(ErrorMessage = "A descrição é obrigatória")]
    [MinLength(4, ErrorMessage = "A identificação deve conter, no mínimo, 4 caracteres")]
    [MaxLength(50, ErrorMessage = "A identificação deve conter, no máximo, 30 caracteres")]
    public string Description { get; set; } = null!;

    public string NormalizedIdentification
    {
        get => Identification is not null
            ? Identification.ToUpper().Replace(" ", "_")
            : string.Empty;
    }
}