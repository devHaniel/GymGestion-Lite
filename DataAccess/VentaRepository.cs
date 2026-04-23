using Dapper;
using DataAccess;
using Gimnasio.Entities;
using Gimnasio.Entities.ViewModels;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Entities.VistaModelos;

namespace Gimnasio.DataAccess
{
    public class VentaRepository
    {
        private readonly MembresiaRepository _membresiaRepository = new MembresiaRepository();
        private readonly PlanMembresiaRepository _planMembresia = new PlanMembresiaRepository();
        public List<VentasVM> ObtenerTodas()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<VentasVM>(
                    "SELECT * FROM VW_VENTAS ORDER BY Fecha DESC"
                ).ToList();
            }
        }

        public List<VentaDetalleVM> ObtenerPorIdVWDetalles(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<VentaDetalleVM>(
                    "SELECT * FROM VW_VENTAS_DETALLE WHERE venta_id = @id ORDER BY Fecha DESC",
                    new {id = id}
                ).ToList();
            }
        }

        public VentasVM ObtenerPorId(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<VentasVM>(
                    "SELECT * FROM VW_VENTAS WHERE id = @id ORDER BY Fecha DESC",
                    new { id = id }
                );
            }
        }
        public List<VentasVM> ObtenerPorCorte(int corteId)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<VentasVM>(
                    "SELECT * FROM VW_VENTAS WHERE Corte_Id = @Corte_Id ORDER BY Fecha DESC",
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
                    "SELECT * FROM VW_VENTAS_DETALLE WHERE Cliente_Id = @ClienteId ORDER BY Fecha DESC",
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
                                (Corte_Id, Cliente_Id, Usuario_Id, Plan_Id, Fecha,
                                 Subtotal, Descuento, Total, Metodo_Pago, Tipo_Venta)
                              VALUES
                                (@Corte_Id, @Cliente_Id, @Usuario_Id, @Plan_Id, @Fecha,
                                 @Subtotal, @Descuento, @Total, @Metodo_Pago, @Tipo_Venta);
                              SELECT SCOPE_IDENTITY();",
                            venta, trx
                        );

                        if(!venta.Tipo_Venta.ToLower().Contains("membresia"))
                        {

                            foreach (var item in detalle)
                            {
                                item.Venta_Id = ventaId;

                                con.Execute(
                                    @"INSERT INTO DETALLE_VENTAS
                                        (Venta_Id, Producto_Id, Cantidad, Precio_Unitario)
                                      VALUES
                                        (@Venta_Id, @Producto_Id, @Cantidad, @Precio_Unitario);",
                                    item, trx
                                );

                                // Descontar stock
                                con.Execute(
                                    "UPDATE PRODUCTOS SET Stock_Actual = Stock_Actual - @Cantidad WHERE Id = @Producto_Id",
                                    new { item.Cantidad, item.Producto_Id }, trx
                                );
                            }
                        }
                        else
                        {
                            var result = _planMembresia.ObtenerPorId((int)venta.Plan_id);
                            var fechaInicio = DateTime.Now;
                            var fechaFin = fechaInicio.AddDays(result.Duracion_Dias); // o plan.Duracion_Dias

                            Membresia membresia = new Membresia()
                            {
                                Venta_Id = ventaId,
                                Cliente_Id = venta.Cliente_Id,
                                Plan_Id = (int)venta.Plan_id,
                                Fecha_Inicio = fechaInicio,
                                Fecha_Fin = fechaFin,
                                Precio_Pagado = venta.Total
                            };
                            _membresiaRepository.Insertar(membresia);
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