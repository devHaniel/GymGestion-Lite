using Dapper;
using DataAccess;
using Entities;
using Gimnasio.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Gimnasio.DataAccess
{
    public class CategoriaRepository
    {
        public List<Categoria> ObtenerTodos()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<Categoria>(
                    "SELECT * FROM CATEGORIAS ORDER BY id desc"
                ).ToList();
            }
        }

        public Categoria ObtenerPorId(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<Categoria>(
                    "SELECT * FROM CATEGORIAS WHERE Id = @Id", new { Id = id }
                );
            }
        }

        public int Insertar(Categoria categoria)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.ExecuteScalar<int>(
                    @"INSERT INTO CATEGORIAS (Nombre)
                      VALUES (@Nombre);
                      SELECT SCOPE_IDENTITY();",
                    categoria
                );
            }
        }

        public bool Actualizar(Categoria categoria)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    @"UPDATE CATEGORIAS SET
                        Nombre = @Nombre
                      WHERE Id = @Id",
                    categoria
                );
                return filas > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    "DELETE FROM CATEGORIAS WHERE Id = @Id",
                    new { Id = id }
                );
                return filas > 0;
            }
        }
    }
}