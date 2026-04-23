using Dapper;
using DataAccess;
using Entities.VistaModelos;
using Gimnasio.Entities;
using Gimnasio.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Gimnasio.DataAccess
{
    public class CompraRepository
    {
        public List<CompraVM> ObtenerTodas()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<CompraVM>(
                    "SELECT * FROM VW_COMPRAS ORDER BY Fecha DESC"
                ).AsList();
            }
        }

        public CompraVM ObtenerPorId(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<CompraVM>(
                    "SELECT * FROM VW_COMPRAS WHERE Id = @Id",
                    new { Id = id }
                );
            }
        }
        public List<CompraDetalleVM> ObtenerPorCorte(int corteId)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {

                return con.Query<CompraDetalleVM>(
                    "SELECT * FROM VW_COMPRAS_DETALLE WHERE Corte_Id = @Corte_Id ORDER BY Fecha DESC",
                    new { Corte_Id = corteId }
                ).AsList();
        
            }
        }

        public List<CompraDetalleVM> ObtenerPorIdVWDetalles(int compraId)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {

                return con.Query<CompraDetalleVM>(
                    "SELECT * FROM VW_COMPRAS_DETALLE WHERE Compra_Id = @Compra_Id ORDER BY Fecha DESC",
                    new { Compra_Id = compraId }
                ).AsList();

            }
        }

        public List<CompraDetalleVM> ObtenerPorFecha(DateTime desde, DateTime hasta)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<CompraDetalleVM>(
                    @"SELECT * FROM VW_COMPRAS_DETALLE
                  WHERE Fecha >= @Desde AND Fecha <= @Hasta
                  ORDER BY Fecha DESC",
                    new { Desde = desde, Hasta = hasta }
                ).AsList();

            }
        }

        /// <summary>
        /// Inserta la compra y su detalle en una sola transacción.
        /// Incrementa el stock de cada producto comprado.
        /// Retorna el Id de la compra generada.
        /// </summary>
        public int Insertar(Compra compra, List<DetalleCompra> detalle)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                con.Open();
                using (var trx = con.BeginTransaction())
                {
                    try
                    {
                        int compra_Id = con.ExecuteScalar<int>(
                            @"INSERT INTO COMPRAS
                        (Corte_Id, Proveedor_Id, Usuario_Id, Fecha, Total, Estado, Notas)
                      VALUES
                        (@Corte_Id, @Proveedor_Id, @Usuario_Id, @Fecha, @Total, @Estado, @Notas);
                      SELECT SCOPE_IDENTITY();",
                            compra, trx
                        );

                        foreach (var item in detalle)
                        {
                            item.Compra_Id = compra_Id;
                            con.Execute(
                                @"INSERT INTO DETALLE_COMPRAS
                            (Compra_Id, Producto_Id, Cantidad, Precio_Unitario)
                          VALUES
                            (@Compra_Id, @Producto_Id, @Cantidad, @Precio_Unitario);",
                                item, trx
                            );

                            // Incrementar stock
                            con.Execute(
                                "UPDATE PRODUCTOS SET Stock_Actual = Stock_Actual + @Cantidad WHERE Id = @Producto_Id",
                                new { item.Cantidad, item.Producto_Id }, trx
                            );
                        }

                        trx.Commit();
                        return compra_Id;
                    }
                    catch
                    {
                        trx.Rollback();
                        throw;
                    }
                }
            }

        }

        public bool ActualizarEstado(int id, string estado)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    "UPDATE COMPRAS SET Estado = @Estado WHERE Id = @Id",
                    new { Id = id, Estado = estado }
                );
                return filas > 0;

            }
        }
    }
}
