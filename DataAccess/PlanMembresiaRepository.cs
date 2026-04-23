using Dapper;
using DataAccess;
using Gimnasio.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Gimnasio.DataAccess
{
    public class PlanMembresiaRepository
    {
        public List<PlanMembresia> ObtenerTodos()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<PlanMembresia>(
                    "SELECT * FROM PLANES_MEMBRESIA WHERE Activo = 1 ORDER BY Precio"
                ).ToList();
            }
        }

        public PlanMembresia ObtenerPorId(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<PlanMembresia>(
                    "SELECT * FROM PLANES_MEMBRESIA WHERE Id = @Id", new { Id = id }
                );
            }
        }

        public int Insertar(PlanMembresia plan)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.ExecuteScalar<int>(
                    @"INSERT INTO PLANES_MEMBRESIA
                        (Nombre, Descripcion, Precio, Duracion_Dias, Activo)
                      VALUES
                        (@Nombre, @Descripcion, @Precio, @Duracion_Dias, @Activo);
                      SELECT SCOPE_IDENTITY();",
                    plan
                );
            }
        }

        public bool Actualizar(PlanMembresia plan)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    @"UPDATE PLANES_MEMBRESIA SET
                        Nombre       = @Nombre,
                        Descripcion  = @Descripcion,
                        Precio       = @Precio,
                        Duracion_Dias = @Duracion_Dias
                      WHERE Id = @Id",
                    plan
                );
                return filas > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    "UPDATE PLANES_MEMBRESIA SET Activo = 0 WHERE Id = @Id",
                    new { Id = id }
                );
                return filas > 0;
            }
        }
    }
}