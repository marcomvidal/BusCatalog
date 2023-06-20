using SantoAndreOnBus.Api.Business.Companies;
using SantoAndreOnBus.Api.Business.Vehicles;
using System.ComponentModel.DataAnnotations;

namespace SantoAndreOnBus.Api.Business.Lines;

public record LineSubmitRequest
{
    public int Id { get; set; }

    [Required(ErrorMessage = "A letra de identificação da linha é obrigatória")]
    [MinLength(1, ErrorMessage = "O nome deve conter, no mínimo, 1 caractere")]
    [MaxLength(2, ErrorMessage = "O nome deve conter, no máximo, 2 caracteres")]
    public string? Letter { get; set; }

    [Required(ErrorMessage = "O número de identificação da linha é obrigatório")]
    [MinLength(2, ErrorMessage = "O número deve conter, no mínimo, 2 caracteres")]
    [MaxLength(4, ErrorMessage = "O número deve conter, no máximo, 4 caracteres")]
    public string? Number { get; set; }

    [Required(ErrorMessage = "O itinerário de ida é obrigatório")]
    [MinLength(3, ErrorMessage = "O itinerário de ida deve conter, no mínimo, 3 caracteres")]
    [MaxLength(50, ErrorMessage = "O itinerário de ida deve conter, no máximo, 50 caracteres")]
    public string? Fromwards { get; set; }
    
    [Required(ErrorMessage = "O itinerário de volta é obrigatório")]
    [MinLength(3, ErrorMessage = "O itinerário de volta deve conter, no mínimo, 3 caracteres")]
    [MaxLength(50, ErrorMessage = "O itinerário de volta deve conter, no máximo, 50 caracteres")]
    public string? Towards { get; set; }

    [Required(ErrorMessage = "O número de partidas é obrigatório")]
    public int Departures { get; set; }
    
    [Required(ErrorMessage = "O intervalo de pico é obrigatório")]
    public int PeakHeadway { get; set; }

    [Required(ErrorMessage = "É obrigatório incluir a empresa vinculada à linha")]
    public int CompanyId { get; set; }
    
    public Company? Company { get; set; }
    public List<InterestPoint>? InterestPoints { get; set; }
    public List<Place>? Places { get; set; }
    public List<Vehicle>? Vehicles { get; set; }

    public override string ToString()
    {
        return $"{Letter}-{Number}";
    }
}
