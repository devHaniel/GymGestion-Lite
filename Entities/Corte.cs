using System;

namespace Gimnasio.Entities
{
    public class Corte
    {
        public int      Id                  { get; set; }
        public int      Usuario_Id           { get; set; }
        public string Nombre_Cliente { get; set; }
        public DateTime Fecha_Apertura       { get; set; } = DateTime.Now;
        public DateTime? Fecha_Cierre        { get; set; }
        public decimal  Monto_Inicial        { get; set; }
        public decimal  Total_Ventas         { get; set; }
        public decimal  Total_Compras        { get; set; }
        public decimal  Total_Efectivo       { get; set; }
        public decimal  Total_Tarjeta        { get; set; }
        public decimal  Total_Transferencia  { get; set; }
        public decimal  Total_Membresias     { get; set; }
        public decimal  Total_Productos      { get; set; }
        public decimal  Gran_Total           { get; set; }
        public string   Estado              { get; set; } = "abierto"; // abierto | cerrado
        public string  Observaciones       { get; set; }
    }
}
