using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto.Presentation.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Models
{
    public class CriarUsuarioModel
    {
        [MinLength(6, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o nome do usuário.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Informe um email válido.")]
        [Required(ErrorMessage = "Informe o email do usuário.")]
        public string Email { get; set; }

        [SenhaValidator(ErrorMessage = "Informe uma senha válida.")]
        [MinLength(8, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe a senha do usuário.")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
        [Required(ErrorMessage = "Confirme a senha do usuário.")]
        public string SenhaConfirmacao { get; set; }

        [Required(ErrorMessage = "Selecione um perfil.")]
        public int IdPerfil { get; set; }

        public List<SelectListItem> ListagemPerfis { get; set; }

    }
}
