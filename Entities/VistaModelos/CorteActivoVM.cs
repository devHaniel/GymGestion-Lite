using System;

namespace Gimnasio.Entities.ViewModels
{
    /// <summary>Espejo de VW_CORTE_ACTIVO</summary>
    public class CorteActivoVM
    {
        public int      Corte_Id             { get; set; }
        public DateTime Fecha_Apertura       { get; set; }
        public decimal  Monto_Inicial        { get; set; }
        public string   Estado              { get; set; } = string.Empty;
        public string   Cajero              { get; set; } = string.Empty;
        public int      Total_Transacciones  { get; set; }
        public decimal  Ventas_Acumuladas    { get; set; }
        public decimal  Efectivo            { get; set; }
        public decimal  Tarjeta             { get; set; }
        public decimal  Transferencia       { get; set; }
        public decimal  Por_Membresias       { get; set; }
        public decimal  Por_Productos        { get; set; }
        public decimal  Por_Mixtas           { get; set; }
        public decimal Total_Compras { get; set; }

        public decimal TotalEnCaja => Monto_Inicial + Efectivo;
    }
}
