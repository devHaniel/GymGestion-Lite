namespace Gimnasio.Entities
{
    public class DetalleCompra
    {
        public int     Id             { get; set; }
        public int     Compra_Id       { get; set; }
        public int     Producto_Id     { get; set; }
        public int     Cantidad       { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Subtotal       => Cantidad * Precio_Unitario; // calculado en memoria, persisted en BD
    }
}
