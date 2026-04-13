using Dapper;
using DataAccess;
using Gimnasio.Entities;
using Gimnasio.Entities.ViewModels;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using Entities.VistaModelos;

namespace Gimnasio.DataAccess
{
    public class ProductoRepository
    {
        public List<ProductoVM> ObtenerTodos()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<ProductoVM>(
                    "SELECT * FROM [VW_PRODUCTOS] WHERE Activo = 1 ORDER BY  Nombre"
                ).ToList();
            }
        }

        public ProductoVM ObtenerPorId(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<ProductoVM>(
                    "SELECT * FROM [VW_PRODUCTOS] WHERE Id = @Id", new { Id = id }
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
                        (Nombre, Categoria_id, Precio_Venta, Precio_Costo, Stock_Actual, Stock_Minimo, Activo)
                      VALUES
                        (@Nombre, @Categoria_id, @Precio_Venta, @Precio_Costo, @Stock_Actual, @Stock_Minimo, @Activo);
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
                        Categoria_id   = @Categoria_id,
                        Precio_Venta = @Precio_Venta,
                        Precio_Costo = @Precio_Costo,
                        Stock_Minimo = @Stock_Minimo
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
                    "UPDATE PRODUCTOS SET Stock_Minimo = @Stock WHERE Id = @Id",
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