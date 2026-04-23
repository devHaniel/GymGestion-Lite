using System;

namespace Gimnasio.Entities
{
    public class Membresia
    {
        public int      Id           { get; set; }
        public int      Cliente_Id    { get; set; }
        public int      Plan_Id       { get; set; }
        public int Venta_Id { get; set; }
        public DateTime Fecha_Inicio { get; set; } = DateTime.Now;
        public DateTime Fecha_Fin     { get; set; }
        public decimal  Precio_Pagado { get; set; }
        public DateTime CreatedAt    { get; set; } = DateTime.Now;
    }
}
