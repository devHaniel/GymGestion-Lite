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
    public class CompraService
    {
        private readonly CompraRepository _compraRepository;

        public CompraService()
        {
            _compraRepository = new CompraRepository();
        }

        public List<CompraDetalleVM> ObtenerPorCorte(int corteId)
        {
           return _compraRepository.ObtenerPorCorte(corteId);
        }

        public List<CompraDetalleVM> ObtenerPorFecha(DateTime desde, DateTime hasta)
        {
            return _compraRepository.ObtenerPorFecha(desde, hasta);
        }

        public int Insertar(Compra compra, List<DetalleCompra> detalle)
        {
            if(compra == null || detalle == null) return -1;
            
            return _compraRepository.Insertar(compra, detalle);

        }

        public bool ActualizarEstado(int id, string estado)
        {
            return _compraRepository.ActualizarEstado(id, estado);
        }
    }
}
