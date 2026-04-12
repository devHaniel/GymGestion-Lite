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
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        public List<Usuario> ObtenerTodos()
        {
            return _usuarioRepository.ObtenerTodos();
        }

        public Usuario ObtenerPorId(int id)
        {
            return _usuarioRepository.ObtenerPorId(id);
        }

        public Usuario ObtenerPorEmail(string email)
        {
            return _usuarioRepository.ObtenerPorEmail(email);
        }

        public int Insertar(Usuario usuario)
        {
            return _usuarioRepository.Insertar(usuario);
        }

        public bool Actualizar(Usuario usuario)
        {
            return _usuarioRepository.Actualizar(usuario);
        }

        public bool ActualizarPassword(int id, string passwordHash)
        {
            return _usuarioRepository.ActualizarPassword(id, passwordHash);
        }

        public bool Eliminar(int id)
        {
            return _usuarioRepository.Eliminar(id);
        }

        public bool Activar(int id)
        {
            return _usuarioRepository.Activar(id);
        }
    }
}
