using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.VistaModelos
{
    public class CompraVM
    {
        public int Id { get; set; }
        public string Estado {  get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int Proveedor_Id { get; set; }
        public string Proveedor { get; set; }
        public int Usuario_Id { get; set; }
        public string Usuario { get; set; }
    }
}
