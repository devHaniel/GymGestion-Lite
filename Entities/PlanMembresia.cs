namespace Gimnasio.Entities
{
    public class PlanMembresia
    {
        public int     Id           { get; set; }
        public string  Nombre       { get; set; } = string.Empty;
        public string Descripcion  { get; set; }
        public decimal Precio       { get; set; }
        public int      Duracion_Dias { get; set; }
        public bool    Activo       { get; set; } = true;
    }
}
