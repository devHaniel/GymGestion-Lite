using Dapper;
using DataAccess;
using Gimnasio.Entities;
using Gimnasio.Entities.ViewModels;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Gimnasio.DataAccess
{
    public class CompraRepository
    {
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
                        int compraId = con.ExecuteScalar<int>(
                            @"INSERT INTO COMPRAS
                        (CorteId, ProveedorId, UsuarioId, Fecha, Total, Estado, Notas)
                      VALUES
                        (@CorteId, @ProveedorId, @UsuarioId, @Fecha, @Total, @Estado, @Notas);
                      SELECT SCOPE_IDENTITY();",
                            compra, trx
                        );

                        foreach (var item in detalle)
                        {
                            item.CompraId = compraId;
                            con.Execute(
                                @"INSERT INTO DETALLE_COMPRAS
                            (CompraId, ProductoId, Cantidad, PrecioUnitario)
                          VALUES
                            (@CompraId, @ProductoId, @Cantidad, @PrecioUnitario);",
                                item, trx
                            );

                            // Incrementar stock
                            con.Execute(
                                "UPDATE PRODUCTOS SET StockActual = StockActual + @Cantidad WHERE Id = @ProductoId",
                                new { item.Cantidad, item.ProductoId }, trx
                            );
                        }

                        trx.Commit();
                        return compraId;
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
