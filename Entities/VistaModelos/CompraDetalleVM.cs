using System;

namespace Gimnasio.Entities.ViewModels
{
    /// <summary>Espejo de VW_COMPRAS_DETALLE</summary>
    public class CompraDetalleVM
    {
        public int      CompraId       { get; set; }
        public DateTime Fecha          { get; set; }
        public string   Estado         { get; set; } = string.Empty;
        public string   Notas          { get; set; }
        public decimal  TotalCompra    { get; set; }
        public int      ProveedorId    { get; set; }
        public string   Proveedor      { get; set; } = string.Empty;
        public string   Usuario        { get; set; } = string.Empty;
        public int      CorteId        { get; set; }
        public DateTime FechaCorte     { get; set; }
        public int      ProductoId     { get; set; }
        public string   Producto       { get; set; } = string.Empty;
        public string   Categoria      { get; set; }
        public int      Cantidad       { get; set; }
        public decimal  PrecioUnitario { get; set; }
        public decimal  Subtotal       { get; set; }
    }
}
