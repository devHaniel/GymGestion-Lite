using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.VistaModelos
{
    public class ProductoVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio_Venta { get; set; }
        public decimal Precio_Costo { get; set; }
        public int Stock_Actual { get; set; }
        public int Stock_Minimo { get; set; }
        public bool Activo { get; set; }
        public int Categoria_Id { get; set; }
        public string Categoria { get; set; }
    }
}
