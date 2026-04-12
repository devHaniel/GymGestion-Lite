using System;

namespace Gimnasio.Entities.ViewModels
{
    /// <summary>Espejo de VW_VENTAS_DETALLE</summary>
    public class VentaDetalleVM
    {
        public int      Venta_Id      { get; set; }
        public DateTime Fecha        { get; set; }
        public string   Tipo_Venta    { get; set; } = string.Empty;
        public string   Metodo_Pago   { get; set; } = string.Empty;
        public decimal  Descuento    { get; set; }
        public decimal  Total_Venta   { get; set; }
        public int      Cliente_Id    { get; set; }
        public string   Cliente      { get; set; } = string.Empty;
        public string   Usuario      { get; set; } = string.Empty;
        public int      Corte_Id      { get; set; }
        public DateTime Fecha_Corte   { get; set; }
        public int? Producto_Id { get; set; }  // NULL si es membresía
        public string Concepto { get; set; } = string.Empty; // nombre producto o plan
        public string LineaTipo { get; set; } = string.Empty; // "producto" | "membresia"
        public string   Categoria    { get; set; }
        public int      Cantidad     { get; set; }
        public decimal  Precio_Unitario { get; set; }
        public decimal  Subtotal     { get; set; }
    }
}
