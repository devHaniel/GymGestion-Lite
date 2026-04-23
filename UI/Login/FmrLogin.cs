using BusinessLogic;
using BusinessLogic.Utils;
using BusinessLogic.Validaciones;
using Gimnasio.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Usuarios;

namespace UI.Login
{
    public partial class FmrLogin : Form
    {
        private readonly UsuarioService _usuarioService;
        public Usuario Usuario { get; set; }
        public FmrLogin()
        {
            InitializeComponent();
            _usuarioService = new UsuarioService();
            VerificarExistencia();
        }

        private void VerificarExistencia()
        {
            var usuarios = _usuarioService.ObtenerTodos();
            if(usuarios.Count == 0)
            {
                FmrUsuariosDetalle fmrUsuariosDetalle = new FmrUsuariosDetalle() { Nuevo = true };
                fmrUsuariosDetalle.Modo();
                fmrUsuariosDetalle.ShowDialog();
            }
        }

        private void cbMostrar_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMostrar.Checked)
            {
                txtContra.PasswordChar = '\0';
            }
            else
                txtContra.PasswordChar = '*';
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            VerificarUsuario();
        }

        private void VerificarUsuario()
        {
            if (!Validaciones.EsCorreoValido(txtCorreo.Text))
            {
                errorProvider1.SetError(txtCorreo, "Correo inválido");
                return;
            }
            var correo = txtCorreo.Text;
            var contra = txtContra.Text;

            if (string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Ingrese un correo.");
                return;
            }

            if (string.IsNullOrWhiteSpace(contra))
            {
                MessageBox.Show("Ingrese una contraseña.");
                return;
            }

            var usuario = _usuarioService.ObtenerPorEmail(correo);

            if (usuario == null)
            {
                MessageBox.Show("Correo o contraseña incorrectas.");
                return;
            }

            var contraEncriptada = Encriptacion.HashSHA256(contra);

            if (!string.IsNullOrEmpty(usuario.Password_Hash) &&
                usuario.Password_Hash.Equals(contraEncriptada))
            {
                this.Usuario = usuario;
                this.DialogResult = DialogResult.OK; 
            }
            else
            {
                MessageBox.Show("Correo o contraseña incorrectas.");
            }
        }
    }
}
