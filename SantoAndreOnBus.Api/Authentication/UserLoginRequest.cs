using System.ComponentModel.DataAnnotations;

namespace SantoAndreOnBus.Api.Authentication;

public class UserLoginRequest
{
    [Required(ErrorMessage = "O e-mail é obrigatório")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [MinLength(5, ErrorMessage = "A senha deve conter, no mínimo, 5 caracteres.")]
    [MaxLength(50, ErrorMessage = "A senha deve contter, no máximo, 50 caracteres.")]
    public string? Password { get; set;}
}
