using Dapper;
using DataAccess;
using Gimnasio.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Gimnasio.DataAccess
{
    public class UsuarioRepository
    {
        public List<Usuario> ObtenerTodos()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<Usuario>(
                    "SELECT * FROM USUARIOS  ORDER BY id desc"
                ).ToList();
            }
        }

        public Usuario ObtenerPorId(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<Usuario>(
                    "SELECT * FROM USUARIOS WHERE Id = @Id", new { Id = id }
                );
            }
        }

        public Usuario ObtenerPorEmail(string email)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<Usuario>(
                    "SELECT * FROM USUARIOS WHERE Email = @Email AND Activo = 1",
                    new { Email = email }
                );
            }
        }

        public int Insertar(Usuario usuario)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.ExecuteScalar<int>(
                    @"INSERT INTO USUARIOS
                        (Nombre, Apellido, Email, Telefono, Rol, password_hash, Activo)
                      VALUES
                        (@Nombre, @Apellido, @Email, @Telefono, @Rol, @Password_Hash, @Activo);
                      SELECT SCOPE_IDENTITY();",
                    usuario
                );
            }
        }

        public bool Actualizar(Usuario usuario)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    @"UPDATE USUARIOS SET
                        Nombre   = @Nombre,
                        Apellido = @Apellido,
                        Email    = @Email,
                        Telefono = @Telefono,
                        Rol      = @Rol
                      WHERE Id = @Id",
                    usuario
                );
                return filas > 0;
            }
        }

        public bool ActualizarPassword(int id, string passwordHash)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    "UPDATE USUARIOS SET Password_Hash = @Password_Hash WHERE Id = @Id",
                    new { Id = id, Password_Hash = passwordHash }
                );
                return filas > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    "UPDATE USUARIOS SET Activo = 0 WHERE Id = @Id",
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
                    "UPDATE USUARIOS SET Activo = 1 WHERE Id = @Id",
                    new { Id = id }
                );
                return filas > 0;
            }
        }
    }
}