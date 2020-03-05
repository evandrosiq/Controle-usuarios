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
    public class PerfilRepository : IPerfilRepository
    {
        private readonly string connectionString;

        public PerfilRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(Perfil entity)
        {
            var query = "insert into Perfil(Nome) values(@Nome)";
            using(var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Update(Perfil entity)
        {
            var query = "update Perfil set Nome = @Nome where IdPerfil = @IdPerfil";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public void Delete(Perfil entity)
        {
            var query = "Delete from Perfil where IdPerfil @IdPerfil";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, entity);
            }
        }

        public List<Perfil> FindAll()
        {
            var query = "select * from Perfil";
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Perfil>(query).ToList();
            }
        }

        public Perfil FindById(int id)
        {
            var query = "select * from Perfil where IdPerfil = @IdPerfil";
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Perfil>
                    (query, new { IdPerfil = id }).FirstOrDefault();
            }
        }      
    }
}
