using System;

namespace Gimnasio.Entities.ViewModels
{
    /// <summary>Espejo de VW_MEMBRESIAS_ACTIVAS</summary>
    public class MembresiaActivaVM
    {
        public int      Membresia_Id   { get; set; }
        public DateTime Fecha_Inicio   { get; set; }
        public DateTime Fecha_Fin      { get; set; }
        public decimal  Precio_Pagado  { get; set; }
        public int      Dias_Restantes { get; set; }
        public int      Cliente_Id     { get; set; }
        public string   Cliente       { get; set; } = string.Empty;
        public string  Telefono      { get; set; }
        public string  Email         { get; set; }
        public int      Plan_Id        { get; set; }
        public string   Plan_n          { get; set; } = string.Empty;
        public int      Duracion_Dias  { get; set; }

    }
}
