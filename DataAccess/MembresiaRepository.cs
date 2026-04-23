using Dapper;
using DataAccess;
using Gimnasio.Entities;
using Gimnasio.Entities.ViewModels;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Gimnasio.DataAccess
{
    public class MembresiaRepository
    {
        public List<MembresiaActivaVM> ObtenerActivas()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<MembresiaActivaVM>(
                    "SELECT * FROM VW_MEMBRESIAS_ACTIVAS ORDER BY DiasRestantes"
                ).ToList();
            }
        }

        public MembresiaActivaVM ObtenerActivaPorCliente(int clienteId)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<MembresiaActivaVM>(
                    "SELECT * FROM VW_MEMBRESIAS_ACTIVAS WHERE Cliente_Id = @Cliente_Id",
                    new { Cliente_Id = clienteId }
                );
            }
        }

        public List<Membresia> ObtenerPorCliente(int clienteId)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<Membresia>(
                    @"SELECT * FROM MEMBRESIAS
                      WHERE Cliente_Id = @Cliente_Id
                      ORDER BY Fecha_Inicio DESC",
                    new { ClienteId = clienteId }
                ).ToList();
            }
        }

        public Membresia ObtenerPorId(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<Membresia>(
                    "SELECT * FROM MEMBRESIAS WHERE Id = @Id", new { Id = id }
                );
            }
        }

        public int Insertar(Membresia membresia)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.ExecuteScalar<int>(
                    @"INSERT INTO MEMBRESIAS
                        (Cliente_Id, Plan_Id, Fecha_Inicio, Fecha_Fin, Precio_Pagado)
                      VALUES
                        (@Cliente_Id, @Plan_Id, @Fecha_Inicio, @Fecha_Fin, @Precio_Pagado);
                      SELECT SCOPE_IDENTITY();",
                    membresia
                );
            }
        }

        public bool ActualizarEstado(int id, string estado)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    "UPDATE MEMBRESIAS SET Estado = @Estado WHERE Id = @Id",
                    new { Id = id, Estado = estado }
                );
                return filas > 0;
            }
        }
    }
}