using DataAccess;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class VisitaService
    {
        private readonly VisitaRepository _repo;

        public VisitaService()
        {
            _repo = new VisitaRepository();
        }

        public List<Visita> ObtenerTodas()
        {
            return _repo.ObtenerTodos();
        }

        public Visita ObtenerPorId(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0.");

            return _repo.ObtenerPorId(id);
        }

        public List<Visita> ObtenerPorCliente(int clienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("El ID del cliente debe ser mayor a 0.");

            return _repo.ObtenerPorCliente(clienteId);
        }

        public int RegistrarVisita(int clienteId)
        {
            if (clienteId <= 0)
                throw new ArgumentException("El ID del cliente debe ser mayor a 0.");

            var visita = new Visita
            {
                Cliente_Id = clienteId,
                Fecha_Ingreso = DateTime.Now
            };

            return _repo.Insertar(visita);
        }

        public bool Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a 0.");

            var existe = _repo.ObtenerPorId(id);
            if (existe == null)
                throw new Exception($"No se encontró una visita con ID {id}.");

            return _repo.Eliminar(id);
        }
    }
}
