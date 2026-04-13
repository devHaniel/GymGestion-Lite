using Dapper;
using DataAccess;
using Gimnasio.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Gimnasio.DataAccess
{
    public class ProveedorRepository
    {
        public List<Proveedor> ObtenerTodos()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<Proveedor>(
                    "SELECT * FROM PROVEEDORES ORDER BY id desc"
                ).ToList();
            }
        }

        public Proveedor ObtenerPorId(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<Proveedor>(
                    "SELECT * FROM PROVEEDORES WHERE Id = @Id", new { Id = id }
                );
            }
        }

        public int Insertar(Proveedor proveedor)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.ExecuteScalar<int>(
                    @"INSERT INTO PROVEEDORES
                        (Nombre, Contacto, Telefono, Email, Direccion)
                      VALUES
                        (@Nombre, @Contacto, @Telefono, @Email, @Direccion);
                      SELECT SCOPE_IDENTITY();",
                    proveedor
                );
            }
        }

        public bool Actualizar(Proveedor proveedor)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    @"UPDATE PROVEEDORES SET
                        Nombre    = @Nombre,
                        Contacto  = @Contacto,
                        Telefono  = @Telefono,
                        Email     = @Email,
                        Direccion = @Direccion
                      WHERE Id = @Id",
                    proveedor
                );
                return filas > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    "DELETE FROM PROVEEDORES WHERE Id = @Id",
                    new { Id = id }
                );
                return filas > 0;
            }
        }
    }
}