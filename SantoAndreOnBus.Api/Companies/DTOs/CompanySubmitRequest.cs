using System.ComponentModel.DataAnnotations;

namespace SantoAndreOnBus.Api.Companies.DTOs;

public record CompanySubmitRequest
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(4, ErrorMessage = "O nome deve conter, no mínimo, 4 caracteres")]
    [MaxLength(50, ErrorMessage = "O nome deve conter, no máximo, 30 caracteres")]
    public string? Name { get; set; } = null!;

    [Required(ErrorMessage = "É obrigatório que haja, pelo menos, um prefixo associado à empresa")]
    public IEnumerable<string> Prefixes { get; set; } = Array.Empty<string>();
}
