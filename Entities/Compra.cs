using System;

namespace Gimnasio.Entities
{
    public class Compra
    {
        public int      Id           { get; set; }
        public int      CorteId      { get; set; }
        public int      ProveedorId  { get; set; }
        public int      UsuarioId    { get; set; }
        public DateTime Fecha        { get; set; } = DateTime.Now;
        public decimal  Total        { get; set; }
        public string   Estado       { get; set; } = "pendiente"; // pendiente | recibida | cancelada
        public string  Notas        { get; set; }
    }
}
