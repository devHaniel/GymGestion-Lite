namespace Gimnasio.Entities
{
    public class DetalleVenta
    {
        public int     Id             { get; set; }
        public int     Venta_Id        { get; set; }
        public int     Producto_Id     { get; set; }
        public int     Cantidad       { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Subtotal       => Cantidad * Precio_Unitario; // calculado en memoria, persisted en BD
    }
}
