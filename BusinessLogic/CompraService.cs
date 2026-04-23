using DataAccess;
using Entities.VistaModelos;
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
        public List<CompraVM> ObtenerTodas()
        {
            return _compraRepository.ObtenerTodas();
        }

        public CompraVM ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0.");

            var compra = _compraRepository.ObtenerPorId(id);

            if (compra == null)
                throw new Exception($"No se encontró una compra con ID {id}.");

            return compra;
        }

        public List<CompraDetalleVM> ObtenerPorIdVWDetalles(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0.");

            var detalles = _compraRepository.ObtenerPorIdVWDetalles(id);

            if (detalles == null)
                throw new Exception($"No se encontraron los detalles con ID {id}.");

            return detalles;

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
