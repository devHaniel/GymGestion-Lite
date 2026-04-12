using Dapper;
using DataAccess;
using Gimnasio.Entities;
using Gimnasio.Entities.ViewModels;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Gimnasio.DataAccess
{
    public class CorteRepository
    {
        public CorteActivoVM ObtenerActivo()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<CorteActivoVM>(
                    "SELECT * FROM VW_CORTE_ACTIVO"
                );
            }
        }

        public Corte ObtenerPorId(int id)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.QueryFirstOrDefault<Corte>(
                    "SELECT * FROM CORTES WHERE Id = @Id", new { Id = id }
                );
            }
        }

        public List<Corte> ObtenerHistorial()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.Query<Corte>(
                    "SELECT * FROM VW_CORTES_CON_CLIENTE ORDER BY Fecha_Apertura DESC"
                ).ToList();
            }
        }

        public int Abrir(Corte corte)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.ExecuteScalar<int>(
                    @"INSERT INTO CORTES
                        (Usuario_Id, Fecha_Apertura, Monto_Inicial, Estado)
                      VALUES
                        (@Usuario_Id, @Fecha_Apertura, @Monto_Inicial, 'abierto');
                      SELECT SCOPE_IDENTITY();",
                    corte
                );
            }
        }

        public bool Cerrar(Corte corte)
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                int filas = con.Execute(
                    @"UPDATE CORTES SET
                        Fecha_Cierre         = @Fecha_Cierre,
                        Total_Ventas         = @Total_Ventas,
                        Total_Compras        = @Total_Compras,
                        Total_Efectivo       = @Total_Efectivo,
                        Total_Tarjeta        = @Total_Tarjeta,
                        Total_Transferencia  = @Total_Transferencia,
                        Total_Membresias     = @Total_Membresias,
                        Total_Productos      = @Total_Productos,
                        Gran_Total           = @Gran_Total,
                        Estado              = 'cerrado',
                        Observaciones       = @Observaciones
                      WHERE Id = @Id",
                    corte
                );
                return filas > 0;
            }
        }

        public bool HayCorteAbierto()
        {
            using (var con = new SqlConnection(Conexion.ConnectionString))
            {
                return con.ExecuteScalar<int>(
                    "SELECT COUNT(1) FROM CORTES WHERE Estado = 'abierto'"
                ) > 0;
            }
        }
    }
}