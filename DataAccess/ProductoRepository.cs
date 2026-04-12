using Dapper;
using DataAccess;
using Gimnasio.Entities;
using Gimnasio.Entities.ViewModels;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Gimnasio.DataAccess
{
    public class ProductoRepository
    {
        public List<Producto> ObtenerTodos()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<Producto>(
                    "SELECT * FROM PRODUCTOS WHERE Activo = 1 ORDER BY Categoria, Nombre"
                ).ToList();
            }
        }

        public Producto ObtenerPorId(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<Producto>(
                    "SELECT * FROM PRODUCTOS WHERE Id = @Id", new { Id = id }
                );
            }
        }

        public List<StockBajoVM> ObtenerStockBajo()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<StockBajoVM>(
                    "SELECT * FROM VW_STOCK_BAJO ORDER BY UnidadesFaltantes DESC"
                ).ToList();
            }
        }

        public int Insertar(Producto producto)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.ExecuteScalar<int>(
                    @"INSERT INTO PRODUCTOS
                        (Nombre, Categoria, PrecioVenta, PrecioCosto, StockActual, StockMinimo, Activo)
                      VALUES
                        (@Nombre, @Categoria, @PrecioVenta, @PrecioCosto, @StockActual, @StockMinimo, @Activo);
                      SELECT SCOPE_IDENTITY();",
                    producto
                );
            }
        }

        public bool Actualizar(Producto producto)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    @"UPDATE PRODUCTOS SET
                        Nombre      = @Nombre,
                        Categoria   = @Categoria,
                        PrecioVenta = @PrecioVenta,
                        PrecioCosto = @PrecioCosto,
                        StockMinimo = @StockMinimo
                      WHERE Id = @Id",
                    producto
                );
                return filas > 0;
            }
        }

        public bool ActualizarStock(int id, int nuevoStock)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    "UPDATE PRODUCTOS SET StockActual = @Stock WHERE Id = @Id",
                    new { Id = id, Stock = nuevoStock }
                );
                return filas > 0;
            }
        }

        public bool Eliminar(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    "UPDATE PRODUCTOS SET Activo = 0 WHERE Id = @Id",
                    new { Id = id }
                );
                return filas > 0;
            }
        }
    }
}