using BusinessLogic.Validaciones;
using Entities;
using Gimnasio.DataAccess;
using Gimnasio.Entities;
using System.Collections.Generic;

namespace Gimnasio.BusinessLogic
{
    public class CategoriaService
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaService()
        {
            _categoriaRepository = new CategoriaRepository();
        }

        public List<Categoria> ObtenerTodos()
        {
            return _categoriaRepository.ObtenerTodos();
        }

        public Categoria ObtenerPorId(int id)
        {
            if (Validaciones.EsNumeroPositivo(id))
            {
                return _categoriaRepository.ObtenerPorId(id);
            }
            return null;
        }

        public int Insertar(Categoria categoria)
        {
            if(Validaciones.LongitudMinima(categoria.Nombre, 2))
                return _categoriaRepository.Insertar(categoria);
            return -1;
        }

        public bool Actualizar(Categoria categoria)
        {
            if (!Validaciones.EsNumeroPositivo(categoria.Id) && !Validaciones.LongitudMinima(categoria.Nombre,2))
            {
                return false;
            }
            return _categoriaRepository.Actualizar(categoria);
        }

        public bool Eliminar(int id)
        {
            if (Validaciones.EsNumeroPositivo(id))
                return _categoriaRepository.Eliminar(id);
            return false;
        }
    }
}