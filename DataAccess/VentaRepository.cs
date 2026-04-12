using Dapper;
using DataAccess;
using Gimnasio.Entities;
using Gimnasio.Entities.ViewModels;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gimnasio.DataAccess
{
    public class VentaRepository
    {
        public List<VentaDetalleVM> ObtenerPorCorte(int corteId)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<VentaDetalleVM>(
                    "SELECT * FROM VW_VENTAS_DETALLE WHERE Corte_Id = @Corte_Id ORDER BY Fecha DESC",
                    new { Corte_Id = corteId }
                ).ToList();
            }
        }

        public List<VentaDetalleVM> ObtenerPorFecha(DateTime desde, DateTime hasta)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<VentaDetalleVM>(
                    @"SELECT * FROM VW_VENTAS_DETALLE
                      WHERE Fecha >= @Desde AND Fecha <= @Hasta
                      ORDER BY Fecha DESC",
                    new { Desde = desde, Hasta = hasta }
                ).ToList();
            }
        }

        public List<VentaDetalleVM> ObtenerPorCliente(int clienteId)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<VentaDetalleVM>(
                    "SELECT * FROM VW_VENTAS_DETALLE WHERE ClienteId = @ClienteId ORDER BY Fecha DESC",
                    new { ClienteId = clienteId }
                ).ToList();
            }
        }

        /// <summary>
        /// Inserta la venta y su detalle en una sola transacción.
        /// Descuenta el stock de cada producto vendido.
        /// Retorna el Id de la venta generada.
        /// </summary>
        public int Insertar(Venta venta, List<DetalleVenta> detalle)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                con.Open();

                using (var trx = con.BeginTransaction())
                {
                    try
                    {
                        int ventaId = con.ExecuteScalar<int>(
                            @"INSERT INTO VENTAS
                                (CorteId, ClienteId, UsuarioId, MembresiaId, Fecha,
                                 Subtotal, Descuento, Total, MetodoPago, TipoVenta)
                              VALUES
                                (@CorteId, @ClienteId, @UsuarioId, @MembresiaId, @Fecha,
                                 @Subtotal, @Descuento, @Total, @MetodoPago, @TipoVenta);
                              SELECT SCOPE_IDENTITY();",
                            venta, trx
                        );

                        foreach (var item in detalle)
                        {
                            item.VentaId = ventaId;

                            con.Execute(
                                @"INSERT INTO DETALLE_VENTAS
                                    (VentaId, ProductoId, Cantidad, PrecioUnitario)
                                  VALUES
                                    (@VentaId, @ProductoId, @Cantidad, @PrecioUnitario);",
                                item, trx
                            );

                            // Descontar stock
                            con.Execute(
                                "UPDATE PRODUCTOS SET StockActual = StockActual - @Cantidad WHERE Id = @ProductoId",
                                new { item.Cantidad, item.ProductoId }, trx
                            );
                        }

                        trx.Commit();
                        return ventaId;
                    }
                    catch
                    {
                        trx.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}