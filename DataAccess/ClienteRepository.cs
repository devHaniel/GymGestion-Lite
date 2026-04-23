using Dapper;
using DataAccess;
using Gimnasio.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Gimnasio.DataAccess
{
    public class ClienteRepository
    {
        public List<Cliente> ObtenerTodos()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<Cliente>(
                    "SELECT * FROM CLIENTES WHERE Activo = 1 ORDER BY Apellido, Nombre"
                ).AsList();

            }
        }

        public Cliente ObtenerPorId(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<Cliente>(
                    "SELECT * FROM CLIENTES WHERE Id = @Id", new { Id = id }
                );

            }
        }

        public List<Cliente> Buscar(string texto, int activo)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<Cliente>(
                    @"SELECT * FROM CLIENTES
                  WHERE Activo = @activo
                    AND (Nombre     LIKE @Texto
                      OR Apellido   LIKE @Texto
                      OR Documento  LIKE @Texto
                      OR Telefono   LIKE @Texto)
                  ORDER BY Apellido, Nombre",
                    new { Texto = $"%{texto}%", activo = activo }
                ).AsList();

            }
        }

        public int Insertar(Cliente cliente)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.ExecuteScalar<int>(
                    @"INSERT INTO CLIENTES
                    (Nombre, Apellido, Email, Telefono, Documento, Fecha_Nacimiento, Activo)
                  VALUES
                    (@Nombre, @Apellido, @Email, @Telefono, @Documento, @Fecha_Nacimiento, @Activo);
                  SELECT SCOPE_IDENTITY();",
                    cliente
                );

            }
        }

        public bool Actualizar(Cliente cliente)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    @"UPDATE CLIENTES SET
                    Nombre          = @Nombre,
                    Apellido        = @Apellido,
                    Email           = @Email,
                    Telefono        = @Telefono,
                    Documento       = @Documento,
                    Fecha_Nacimiento = @Fecha_Nacimiento
                  WHERE Id = @Id",
                    cliente
                );
                return filas > 0;

            }
        }

        public bool Eliminar(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    "UPDATE CLIENTES SET Activo = 0 WHERE Id = @Id",
                    new { Id = id }
                );
            return filas > 0;
            }
        }

        public bool Activar(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    "UPDATE CLIENTES SET Activo = 1 WHERE Id = @Id",
                    new { Id = id }
                );
                return filas > 0;
            }
        }
    }
}
