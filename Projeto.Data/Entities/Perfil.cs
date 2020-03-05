using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Entities
{
    public class Perfil
    {
        public int IdPerfil { get; set; }
        public string Nome { get; set; }

        public List<Usuario> Usuarios { get; set; }
    }
}
