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
            return _categoriaRepository.ObtenerPorId(id);
        }

        public int Insertar(Categoria categoria)
        {
            return _categoriaRepository.Insertar(categoria);
        }

        public bool Actualizar(Categoria categoria)
        {
            return _categoriaRepository.Actualizar(categoria);
        }

        public bool Eliminar(int id)
        {
            return _categoriaRepository.Eliminar(id);
        }
    }
}