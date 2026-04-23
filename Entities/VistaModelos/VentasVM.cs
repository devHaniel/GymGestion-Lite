using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.VistaModelos
{
    public class VentasVM
    {
        public int Id {  get; set; }
        public int Corte_id { get; set; }
        public string Cliente { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string Tipo_venta { get; set; }
    }
}
