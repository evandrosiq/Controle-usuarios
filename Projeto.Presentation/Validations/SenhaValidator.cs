using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Presentation.Validations
{
    public class SenhaValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var senha = value.ToString();

            return senha.Any(char.IsUpper)
                && senha.Any(char.IsLower)
                && senha.Any(char.IsDigit)
                && (senha.Contains("!")
                 || senha.Contains("@")
                 || senha.Contains("#")
                 || senha.Contains("$")
                 || senha.Contains("%")
                 || senha.Contains("&"));
        }
    }
}
