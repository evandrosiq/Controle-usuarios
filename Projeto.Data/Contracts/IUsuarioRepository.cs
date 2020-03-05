using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Data.Contracts
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Usuario Find(string email);
        Usuario Find(string email, string senha);
    }
}
