using DataAccess;
using Gimnasio.DataAccess;
using Gimnasio.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ClienteService
    {
        private readonly ClienteRepository _clienteRepository;
        
        public ClienteService()
        {
            _clienteRepository = new ClienteRepository();
        }
        public List<Cliente> ObtenerTodos()
        {
            return _clienteRepository.ObtenerTodos();
        }
        public Cliente ObtenerPorId(int id)
        {
            return _clienteRepository.ObtenerPorId(id);
        }

        public List<Cliente> Buscar(string texto)
        {
            return _clienteRepository.Buscar(texto);
        }

        public int Insertar(Cliente cliente)
        {
            if(cliente == null)
            {
                return -1;
            }

            return _clienteRepository.Insertar(cliente);
        }

        public bool Actualizar(Cliente cliente)
        {
            if(cliente == null)
            {
                return false;
            }
            
            return _clienteRepository.Actualizar(cliente);
        }

        public bool Eliminar(int id)
        {
            if (id < 0)
                return false;

            return _clienteRepository.Eliminar(id);
        }
    }
}
