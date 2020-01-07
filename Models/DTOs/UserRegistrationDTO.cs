using System.ComponentModel.DataAnnotations;

namespace SantoAndreOnBus.Models.DTOs
{
    public class UserRegistrationDTO
    {
        [Required(ErrorMessage = "O nome de usuário é obrigatório")]
        [EmailAddress(ErrorMessage = "O nome de usuário deve ser um e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [MinLength(5, ErrorMessage = "A senha deve conter, no mínimo, 5 caracteres.")]
        [MaxLength(50, ErrorMessage = "A senha deve contter, no máximo, 50 caracteres.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "A senha e sua respectiva confirmação não conferem")]
        public string PasswordConfirmation { get; set; }
    }
}