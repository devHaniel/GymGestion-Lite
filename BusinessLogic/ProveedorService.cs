using Gimnasio.DataAccess;
using Gimnasio.Entities;
using Gimnasio.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ProveedorService
    {
        private readonly ProveedorRepository _proveedorRepository;
        public ProveedorService()
        {
            _proveedorRepository = new ProveedorRepository();
        }

        public List<Proveedor> ObtenerTodos()
        {
            return _proveedorRepository.ObtenerTodos();
        }

        public Proveedor ObtenerPorId(int id)
        {
            return _proveedorRepository.ObtenerPorId(id);
        }

        public int Insertar(Proveedor proveedor)
        {
            return _proveedorRepository.Insertar(proveedor);
        }

        public bool Actualizar(Proveedor proveedor)
        {
            return _proveedorRepository.Actualizar(proveedor);
        }


        public bool Eliminar(int id)
        {
            return _proveedorRepository.Eliminar(id);
        }
    }
}
