using Dapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class VisitaRepository
    {
        public List<Visita> ObtenerTodos()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<Visita>(
                    "SELECT * FROM VISITAS ORDER BY id DESC"
                ).ToList();
            }
        }

        public Visita ObtenerPorId(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<Visita>(
                    "SELECT * FROM VISITAS WHERE Id = @Id", new { Id = id }
                );
            }
        }

        public List<Visita> ObtenerPorCliente(int clienteId)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<Visita>(
                    "SELECT * FROM VISITAS WHERE cliente_id = @ClienteId ORDER BY fecha_ingreso DESC",
                    new { ClienteId = clienteId }
                ).ToList();
            }
        }

        public int Insertar(Visita visita)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.ExecuteScalar<int>(
                    @"INSERT INTO VISITAS
                        (cliente_id, fecha_ingreso)
                      VALUES
                        (@Cliente_Id, @Fecha_Ingreso);
                      SELECT SCOPE_IDENTITY();",
                    visita
                );
            }
        }

        public bool Eliminar(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    "DELETE FROM VISITAS WHERE Id = @Id",
                    new { Id = id }
                );
                return filas > 0;
            }
        }
    }
}
