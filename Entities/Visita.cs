using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Visita
    {
        public int Id { get; set; }
        public int Cliente_Id { get; set; }
        public DateTime Fecha_Ingreso { get; set; }
    }
}
