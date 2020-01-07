using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SantoAndreOnBus.Models.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(4, ErrorMessage = "O nome deve conter, no mínimo, 4 caracteres")]
        [MaxLength(50, ErrorMessage = "O nome deve conter, no máximo, 30 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "É obrigatório que haja, pelo menos, um prefixo associado à empresa")]
        public List<Prefix> Prefixes { get; set; }
    }
}