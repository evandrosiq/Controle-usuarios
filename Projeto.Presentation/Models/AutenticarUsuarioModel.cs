using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Models
{
    public class AutenticarUsuarioModel
    {
        [EmailAddress(ErrorMessage = "Informe um email válido.")]
        [Required(ErrorMessage = "Informe o email do usuário.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário.")]
        public string Senha { get; set; }
    }
}
