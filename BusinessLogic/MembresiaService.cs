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
    public class MembresiaService
    {
        private readonly MembresiaRepository _membresiaRepository;

        public MembresiaService()
        {
            _membresiaRepository = new MembresiaRepository();
        }

        public List<MembresiaActivaVM> ObtenerActivas()
        {
            return _membresiaRepository.ObtenerActivas();
        }

        public MembresiaActivaVM ObtenerActivaPorCliente(int clienteId)
        {
            return _membresiaRepository.ObtenerActivaPorCliente(clienteId);
        }

        public List<Membresia> ObtenerPorCliente(int clienteId)
        {
            return _membresiaRepository.ObtenerPorCliente(clienteId);
        }

        public Membresia ObtenerPorId(int id)
        {
            return _membresiaRepository.ObtenerPorId(id);
        }

        public int Insertar(Membresia membresia)
        {
            if (membresia == null) return -1;

            return _membresiaRepository.Insertar(membresia);
        }

        public bool ActualizarEstado(int id, string estado)
        {
            return _membresiaRepository.ActualizarEstado(id, estado);
        }
    }
}
