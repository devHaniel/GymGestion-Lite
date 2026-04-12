namespace Gimnasio.Entities
{
    public class Producto
    {
        public int     Id           { get; set; }
        public string  Nombre       { get; set; } = string.Empty;
        public string Categoria    { get; set; }
        public decimal PrecioVenta  { get; set; }
        public decimal PrecioCosto  { get; set; }
        public int     StockActual  { get; set; }
        public int     StockMinimo  { get; set; }
        public bool    Activo       { get; set; } = true;

        public bool StockBajo => StockActual <= StockMinimo;
    }
}
