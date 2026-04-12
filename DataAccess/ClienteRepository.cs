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

        public List<Cliente> Buscar(string texto)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<Cliente>(
                    @"SELECT * FROM CLIENTES
                  WHERE Activo = 1
                    AND (Nombre     LIKE @Texto
                      OR Apellido   LIKE @Texto
                      OR Documento  LIKE @Texto
                      OR Telefono   LIKE @Texto)
                  ORDER BY Apellido, Nombre",
                    new { Texto = $"%{texto}%" }
                ).AsList();

            }
        }

        public int Insertar(Cliente cliente)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.ExecuteScalar<int>(
                    @"INSERT INTO CLIENTES
                    (Nombre, Apellido, Email, Telefono, Documento, FechaNacimiento, Activo)
                  VALUES
                    (@Nombre, @Apellido, @Email, @Telefono, @Documento, @FechaNacimiento, @Activo);
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
                    FechaNacimiento = @FechaNacimiento
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
    }
}
