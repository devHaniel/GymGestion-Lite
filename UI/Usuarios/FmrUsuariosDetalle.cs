using BusinessLogic;
using BusinessLogic.Utils;
using Gimnasio.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Usuarios
{
    public partial class FmrUsuariosDetalle : Form
    {
        private readonly UsuarioService _usuarioService;
        private readonly FmrUsuarios _fmrUsuarios;
        public bool Nuevo { get; set; } = false;
        public bool Editar { get; set; } = false;
        public int IdUsuario { get; set; } = 0;
        public bool Detalles { get; set; } = false;

        public FmrUsuariosDetalle(FmrUsuarios fmrUsuarios)
        {
            InitializeComponent();
            _usuarioService = new UsuarioService();
            _fmrUsuarios = fmrUsuarios;
        }

        public void Modo()
        {
            if (Nuevo)
            {
                lblEstado.Text = "Modo: Nuevo Registro";
                btnVer.Visible = false;
                txtCodigo.Text = "--";
                txtNombre.Focus();
            }
            else if (Editar)
            {
                lblEstado.Text = "Modo: Editar Registro";
                txtCodigo.Text = IdUsuario.ToString();
                CargarDatos();
                txtContra.Clear();
                txtContra.ReadOnly = true;
                txtContra.Enabled = false;
                cmbActivo.Enabled = false;
                cmbInactivo.Enabled = false;
            }
            else if (Detalles)
            {
                lblEstado.Text = "Modo: Detalles del Registro";
                txtNombre.ReadOnly = true;
                txtApellido.ReadOnly = true;
                txtEmail.ReadOnly = true;
                txtTelefono.ReadOnly = true;
                txtContra.ReadOnly = true;
                btnVer.Visible = false;
                btnGuardar.Visible= false;
                btnLimpiar.Visible= false;
                cmbActivo.Enabled = false;
                cmbInactivo.Enabled=false;
                CargarDatos();

            }
        }

        public void CargarDatos()
        {
            if (IdUsuario > 0)
            {
                var Usuario = _usuarioService.ObtenerPorId(IdUsuario);
                txtCodigo.Text = IdUsuario.ToString();
                txtNombre.Text = Usuario.Nombre;
                txtApellido.Text = Usuario.Apellido;
                txtEmail.Text = Usuario.Email;
                txtTelefono.Text = Usuario.Telefono;
                txtContra.Text = "********"; // No mostrar la contraseña real
                if(Usuario.Activo)
                {
                    cmbActivo.Checked = true;
                }
                else
                {
                    cmbInactivo.Checked = true;
                }
            }
        }

        private void MostrarContrasenia()
        {
            var usuario = _usuarioService.ObtenerPorId(IdUsuario);
            string pass_ingresado = Interaction.InputBox(
                "Ingrese su contraseña:",   // mensaje
                "Verificación",             // título
                ""                          // valor por defecto
            ).Trim();


            if (!string.IsNullOrEmpty(pass_ingresado))
            {
                if(usuario.Password_Hash.Equals(Encriptacion.HashSHA256(pass_ingresado)))
                {
                    btnVer.Enabled = false;
                    //txtContra.Text = pass_ingresado;
                    txtContra.ReadOnly = false;
                    txtContra.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Contraseña no coincide.");
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            MostrarContrasenia();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Nuevo)
            {
                GuardarUsuario();
            }
            if (Editar)
            {
                ActualizarUsuario();
            }
        }

        private void GuardarUsuario()
        {
            var nombre = txtNombre.Text;
            var apellido = txtApellido.Text;
            var email = txtEmail.Text;
            var telefono = txtTelefono.Text;
            var password = Encriptacion.HashSHA256(txtContra.Text);
            var activo = cmbActivo.Checked;

            Usuario usuario = new Usuario()
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Telefono = telefono,
                Password_Hash = password,
                Activo = activo
            };

            var result = _usuarioService.Insertar(usuario);

            if(result > 0)
            {
                MessageBox.Show("Usuario ingresado correctamente.");
                _fmrUsuarios.Activos_Inactivos();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error en ingresar al usuario");
            }
        }
        private void ActualizarUsuario()
        {
            bool actualizarPassword = false;

            var codigo = IdUsuario;
            var nombre = txtNombre.Text;
            var apellido = txtApellido.Text;
            var email = txtEmail.Text;
            var telefono = txtTelefono.Text;
            var password = ""; //Encriptacion.HashSHA256(txtContra.Text);

            if(txtContra.Enabled && txtContra.Text.Length > 0)
            {
                password = Encriptacion.HashSHA256(txtContra.Text);
                actualizarPassword = true;
            }

            Usuario usuario = new Usuario()
            {
                    Id = codigo,
                    Nombre = nombre,
                    Apellido = apellido,
                    Email = email,
                    Telefono = telefono,
            };


            if (!_usuarioService.Actualizar(usuario))
            {
                MessageBox.Show("Error al actualizar el usuario.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (actualizarPassword)
                _usuarioService.ActualizarPassword(IdUsuario, password);

            MessageBox.Show("Usuario actualizado correctamente.");
            _fmrUsuarios.Activos_Inactivos();
            this.Close();
        }
    }
}
