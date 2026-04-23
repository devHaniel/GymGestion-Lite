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

        public List<Cliente> Buscar(string texto, int activo)
        {
            return _clienteRepository.Buscar(texto, activo);
        }

        public int Insertar(Cliente cliente)
        {
            if (cliente == null)
                return -1;

            if (!Validaciones.Validaciones.NoVacio(cliente.Nombre))
                return -1;

            if (!Validaciones.Validaciones.EsCorreoValido(cliente.Email))
                return -1;

            return _clienteRepository.Insertar(cliente);
        }

        public bool Actualizar(Cliente cliente)
        {
            if (cliente == null)
                return false;

            if (!Validaciones.Validaciones.EsNumeroPositivo(cliente.Id))
                return false;

            if (!Validaciones.Validaciones.NoVacio(cliente.Nombre))
                return false;

            if (!Validaciones.Validaciones.EsCorreoValido(cliente.Email))
                return false;

            return _clienteRepository.Actualizar(cliente);
        }

        public bool Eliminar(int id)
        {
            if (!Validaciones.Validaciones.EsNumeroPositivo(id))
                return false;


            return _clienteRepository.Eliminar(id);
        }

        public bool Activar(int id)
        {
            if (!Validaciones.Validaciones.EsNumeroPositivo(id))
                return false;

            return _clienteRepository.Activar(id);
        }
    }
}
