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
    public class VentaService
    {
        private readonly VentaRepository _ventaRepository;

        public VentaService()
        {
            _ventaRepository = new VentaRepository();
        }

        public List<VentasVM> ObtenerTodas()
        {
            return _ventaRepository.ObtenerTodas();
        }

        public List<VentaDetalleVM> ObtenerPorIdVWDetalles(int id)
        {
            return _ventaRepository.ObtenerPorIdVWDetalles(id);
        }

        public VentasVM ObtenerPorId(int id)
        {
            return _ventaRepository.ObtenerPorId(id);
        }
        public List<VentasVM> ObtenerPorCorte(int corteId)
        {
            return _ventaRepository.ObtenerPorCorte(corteId);
        }

        public List<VentaDetalleVM> ObtenerPorFecha(DateTime desde, DateTime hasta)
        {
            return _ventaRepository.ObtenerPorFecha(desde, hasta);
        }

        public List<VentaDetalleVM> ObtenerPorCliente(int clienteId)
        {
            return _ventaRepository.ObtenerPorCliente(clienteId);   
        }

        public int Insertar(Venta venta, List<DetalleVenta> detalle)
        {
            if (venta == null) return -1;

            return _ventaRepository.Insertar(venta, detalle);
        }
    }
}
