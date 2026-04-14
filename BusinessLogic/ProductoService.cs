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
    public class ProductoService
    {
        private readonly ProductoRepository _productoRepository;

        public ProductoService()
        {
            _productoRepository = new ProductoRepository();
        }

        public List<ProductoVM> ObtenerTodos()
        {
            return _productoRepository.ObtenerTodos();
        }

        public ProductoVM ObtenerPorId(int id)
        {
            return _productoRepository.ObtenerPorId(id);
        }

        public List<StockBajoVM> ObtenerStockBajo()
        {
            return _productoRepository.ObtenerStockBajo();
        }

        public int Insertar(Producto producto)
        {
            return _productoRepository.Insertar(producto);
        }

        public bool Actualizar(Producto producto)
        {
            return _productoRepository.Actualizar(producto);
        }

        public bool ActualizarStock(int id, int nuevoStock)
        {
            return _productoRepository.ActualizarStock(id, nuevoStock);
        }

        public bool Eliminar(int id)
        {
            return _productoRepository.Eliminar(id);
        }
        public bool Activar(int id)
        {
            return _productoRepository.Activar(id);
        }
    }
}
