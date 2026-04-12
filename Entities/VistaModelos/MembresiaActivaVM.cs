using System;

namespace Gimnasio.Entities.ViewModels
{
    /// <summary>Espejo de VW_MEMBRESIAS_ACTIVAS</summary>
    public class MembresiaActivaVM
    {
        public int      MembresiaId   { get; set; }
        public DateTime FechaInicio   { get; set; }
        public DateTime FechaFin      { get; set; }
        public string   Estado        { get; set; } = string.Empty;
        public decimal  PrecioPagado  { get; set; }
        public int      DiasRestantes { get; set; }
        public int      ClienteId     { get; set; }
        public string   Cliente       { get; set; } = string.Empty;
        public string  Telefono      { get; set; }
        public string  Email         { get; set; }
        public int      PlanId        { get; set; }
        public string   Plan          { get; set; } = string.Empty;
        public int      DuracionDias  { get; set; }

        public bool ProximaAVencer => DiasRestantes <= 7;
    }
}
