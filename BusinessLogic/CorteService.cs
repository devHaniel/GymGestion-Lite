using DataAccess;
using Gimnasio.DataAccess;
using Gimnasio.Entities;
using Gimnasio.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CorteService
    {
        private readonly CorteRepository _corteRepositoy;

        public CorteService()
        {
            _corteRepositoy = new CorteRepository();
        }

        public CorteActivoVM ObtenerActivo()
        {
            return _corteRepositoy.ObtenerActivo();
        }

        public Corte ObtenerPorId(int id)
        {
            return _corteRepositoy.ObtenerPorId(id);    
        }

        public List<Corte> ObtenerHistorial()
        {
            return _corteRepositoy.ObtenerHistorial();
        }

        public int Abrir(Corte corte)
        {
            return _corteRepositoy.Abrir(corte);
        }

        public bool Cerrar(Corte corte)
        {
            return _corteRepositoy.Cerrar(corte);
        }

        public bool HayCorteAbierto()
        {
            return _corteRepositoy.HayCorteAbierto();
        }
    }
}
