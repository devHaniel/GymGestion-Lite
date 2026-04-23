using System;

namespace Gimnasio.Entities
{
    public class Cliente
    {
        public int      Id              { get; set; }
        public string   Nombre          { get; set; } = string.Empty;
        public string   Apellido        { get; set; } = string.Empty;
        public string  Email           { get; set; }
        public string  Telefono        { get; set; }
        public string  Documento       { get; set; }
        public DateTime? Fecha_Nacimiento { get; set; }
        public bool     Activo          { get; set; } = true;
        public DateTime CreatedAt       { get; set; } = DateTime.Now;

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}
