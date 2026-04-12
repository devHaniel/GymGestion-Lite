using System;

namespace Gimnasio.Entities
{
    public class Membresia
    {
        public int      Id           { get; set; }
        public int      Cliente_Id    { get; set; }
        public int      Plan_Id       { get; set; }
        public int Venta_Id { get; set; }
        public DateTime FechaInicio  { get; set; }
        public DateTime FechaFin     { get; set; }
        public string   Estado       { get; set; } = "activa"; // activa | vencida | suspendida | congelada
        public decimal  PrecioPagado { get; set; }
        public DateTime CreatedAt    { get; set; } = DateTime.Now;
    }
}
