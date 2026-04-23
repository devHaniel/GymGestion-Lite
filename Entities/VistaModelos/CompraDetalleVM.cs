using System;

namespace Gimnasio.Entities.ViewModels
{
    /// <summary>Espejo de VW_COMPRAS_DETALLE</summary>
    public class CompraDetalleVM
    {
        public int      Compra_Id       { get; set; }
        public DateTime Fecha          { get; set; }
        public string   Estado         { get; set; } = string.Empty;
        public string   Notas          { get; set; }
        public decimal  Total_Compra    { get; set; }
        public int      Proveedor_Id    { get; set; }
        public string   Proveedor      { get; set; } = string.Empty;
        public string   Usuario        { get; set; } = string.Empty;
        public int      Corte_Id        { get; set; }
        public DateTime Fecha_Corte     { get; set; }
        public int      Producto_Id     { get; set; }
        public string   Producto       { get; set; } = string.Empty;
        public string   Categoria      { get; set; }
        public int      Cantidad       { get; set; }
        public decimal  Precio_Unitario { get; set; }
        public decimal  Subtotal       { get; set; }
    }
}
