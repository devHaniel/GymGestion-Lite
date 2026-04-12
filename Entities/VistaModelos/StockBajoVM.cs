namespace Gimnasio.Entities.ViewModels
{
    /// <summary>Espejo de VW_STOCK_BAJO</summary>
    public class StockBajoVM
    {
        public int     ProductoId        { get; set; }
        public string  Producto          { get; set; } = string.Empty;
        public string  Categoria         { get; set; }
        public int     StockActual       { get; set; }
        public int     StockMinimo       { get; set; }
        public int     UnidadesFaltantes { get; set; }
        public decimal PrecioCosto       { get; set; }
        public decimal PrecioVenta       { get; set; }

        public decimal CostoReposicion => UnidadesFaltantes * PrecioCosto;
    }
}
