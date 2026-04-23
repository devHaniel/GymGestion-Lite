using System;

namespace Gimnasio.Entities
{
    public class Compra
    {
        public int      Id           { get; set; }
        public int      Corte_Id      { get; set; }
        public int      Proveedor_Id  { get; set; }
        public int      Usuario_Id    { get; set; }
        public DateTime Fecha        { get; set; } = DateTime.Now;
        public decimal  Total        { get; set; }
        public string   Estado       { get; set; } = "pendiente"; // pendiente | recibida | cancelada
        public string  Notas        { get; set; }
    }
}
