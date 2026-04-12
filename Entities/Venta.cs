using System;

namespace Gimnasio.Entities
{
    public class Venta
    {
        public int      Id           { get; set; }
        public int      Corte_id      { get; set; }
        public int      Cliente_Id    { get; set; }
        public int      Usuario_Id    { get; set; }
        public int?     Plan_id  { get; set; }
        public DateTime Fecha        { get; set; } = DateTime.Now;
        public decimal  Subtotal     { get; set; }
        public decimal  Descuento    { get; set; }
        public decimal  Total        { get; set; }
        public string   Metodo_Pago   { get; set; } = "efectivo"; // efectivo | tarjeta | transferencia
        public string   Tipo_Venta    { get; set; } = "producto"; // membresia | producto | mixta
    }
}
