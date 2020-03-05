using Dapper;
using Projeto.Data.Contracts;
using Projeto.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Projeto.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string connectionString;

        public UsuarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(Usuario entity)
        {
            var query = "insert into Usuario(Nome, Email, Senha, DataCriacao, Status, IdPerfil) "
                        + "values(@Nome, @Email, @Senha, @DataCriacao, @Status, @IdPerfil)";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }
        public void Update(Usuario entity)
        {
            var query = "update Usuario set Nome = @Nome, Email = @Email, Senha = @Senha, Status = @Status "
                        + "IdPerfil = @IdPerfil where IdUsuario = @IdUsuario";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Delete(Usuario entity)
        {
            var query = "delete from Usuario where IdUsuario = @IdUsuario";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public Usuario Find(string email)
        {
            var query = "select * from Usuario u inner join Perfil p "
                        + "on p.IdPerfil = u.IdPerfil "
                        + "where Email = @Email";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query(query, (Usuario u, Perfil p) => 
                {
                    u.Perfil = p;
                    return u;
                }, new {email},
                splitOn: "IdPerfil").FirstOrDefault();
            }

        }

        public Usuario Find(string email, string senha)
        {
            var query = "select * from Usuario u inner join Perfil p "
                        + "on p.IdPerfil = u.IdPerfil "
                        + "where Email = @Email and Senha = @Senha";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query(query, (Usuario u, Perfil p) =>
                {
                    u.Perfil = p;
                    return u;
                }, new { email, senha },
                splitOn: "IdPerfil").FirstOrDefault();
            }
        }

        public List<Usuario> FindAll()
        {
            var query = "select * from Usuario u inner join Perfil p "
                        + "on p.IdPerfil = u.IdPerfil";
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query(query,
                    (Usuario u, Perfil p) =>
                    {
                        u.Perfil = p;
                        return u;
                    },
                    splitOn: "IdPerfil"
                    ).ToList();
            }
        }

        public Usuario FindById(int id)
        {
            var query = "select * from Usuario u inner join Perfil p "
                        + "on p.IdPerfil = u.IdPerfil "
                        + "where IdUsuario = @IdUsuario";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query(query,
                    (Usuario u, Perfil p) =>
                    {
                        u.Perfil = p;
                        return u;
                    }, new { IdUsuario = id },
                    splitOn: "IdPerfil"
                    ).FirstOrDefault();
            }
        }
    }
}
