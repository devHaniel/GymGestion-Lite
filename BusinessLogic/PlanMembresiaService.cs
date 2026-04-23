using Entities;
using Gimnasio.DataAccess;
using Gimnasio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class PlanMembresiaService
    {
        private readonly PlanMembresiaRepository _planMembresiaRepository;

        public PlanMembresiaService()
        {
            _planMembresiaRepository = new PlanMembresiaRepository();
        }

        public List<PlanMembresia> ObtenerTodos()
        {
            return _planMembresiaRepository.ObtenerTodos();
        }

        public PlanMembresia ObtenerPorId(int id)
        {
            return _planMembresiaRepository.ObtenerPorId(id);
        }

        public int Insertar(PlanMembresia planMembresia)
        {
            return _planMembresiaRepository.Insertar(planMembresia);
        }

        public bool Actualizar(PlanMembresia planMembresia)
        {
            return _planMembresiaRepository.Actualizar(planMembresia);
        }

        public bool Eliminar(int id)
        {
            return _planMembresiaRepository.Eliminar(id);
        }
    }
}
