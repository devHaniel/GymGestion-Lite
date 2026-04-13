namespace Gimnasio.Entities
{
    public class Producto
    {
        public int     Id           { get; set; }
        public string  Nombre       { get; set; } = string.Empty;
        public int Categoria_Id    { get; set; }
        public decimal Precio_Venta  { get; set; }
        public decimal Precio_Costo  { get; set; }
        public int     Stock_Actual  { get; set; }
        public int     Stock_Minimo  { get; set; }
        public bool    Activo       { get; set; } = true;

        public bool StockBajo => Stock_Actual <= Stock_Minimo;
    }
}
