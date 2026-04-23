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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Proveedores
{
    public partial class FmrProveedoresDetalle : Form
    {
        private readonly ProveedorService _proveedorService;
        private readonly FmrProveedores _fmrProveedores;
        public bool Nuevo { get; set; } = false;
        public bool Editar { get; set; } = false;
        public int IdProveedor { get; set; } = 0;
        public bool Detalles { get; set; } = false;
        public FmrProveedoresDetalle(FmrProveedores fmrProveedores)
        {
            InitializeComponent();
            _fmrProveedores = fmrProveedores;
            _proveedorService = new ProveedorService();
        }
        public void Modo()
        {
            if (Nuevo)
            {
                lblEstado.Text = "Modo: Nuevo Registro";
                txtCodigo.Text = "--";
                txtNombre.Focus();
            }
            else if (Editar)
            {
                lblEstado.Text = "Modo: Editar Registro";
                txtCodigo.Text = IdProveedor.ToString();
                CargarDatos();
            }
            else if (Detalles)
            {
                lblEstado.Text = "Modo: Detalles del Registro";
                txtNombre.ReadOnly = true;
                txtContacto.ReadOnly = true;
                txtEmail.ReadOnly = true;
                txtTelefono.ReadOnly = true;
                txtDireccion.ReadOnly = true;
                btnGuardar.Visible = false;
                btnLimpiar.Visible = false;
                CargarDatos();

            }
        }

        public void CargarDatos()
        {
            if (IdProveedor > 0)
            {
                var proveedor = _proveedorService.ObtenerPorId(IdProveedor);
                txtCodigo.Text = proveedor.ToString();
                txtNombre.Text = proveedor.Nombre;
                txtContacto.Text = proveedor.Contacto;
                txtEmail.Text = proveedor.Email;
                txtTelefono.Text = proveedor.Telefono;
                txtDireccion.Text = proveedor.Direccion;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!Validaciones.EsCorreoValido(txtEmail.Text))
            {
                errorProvider1.SetError(txtEmail, "Correo inválido");
                return;
            }
            if (Nuevo)
            {
                GuardarProveedor();
            }
            if (Editar)
            {
                ActualizarProveedor();
            }
        }

        private void ActualizarProveedor()
        {
            var nombre = txtNombre.Text;
            var contacto = txtContacto.Text;
            var email = txtEmail.Text;
            var telefono = txtTelefono.Text;
            var direccion = txtDireccion.Text;

            var proveedor = new Proveedor()
            {
                Id = IdProveedor,
                Nombre = nombre,
                Contacto = contacto,
                Email = email,
                Telefono = telefono,
                Direccion = direccion
            };

            var result = _proveedorService.Actualizar(proveedor);
            if (result)
            {
                MessageBox.Show("Proveedor actualizado correctamente.");
                _fmrProveedores.MostrarProveedores(_proveedorService.ObtenerTodos());
                this.Close();
            }
            else
            {
                MessageBox.Show("Error en actualizar al proveedor");
            }
        }

        private void GuardarProveedor()
        {
            var nombre = txtNombre.Text;
            var contacto = txtContacto.Text;
            var email = txtEmail.Text;
            var telefono = txtTelefono.Text;
            var direccion = txtDireccion.Text;

            var proveedor = new Proveedor()
            {
                Nombre = nombre,
                Contacto = contacto,
                Email = email,
                Telefono = telefono,
                Direccion = direccion
            };

            var result = _proveedorService.Insertar(proveedor);
            if (result > 0)
            {
                MessageBox.Show("Proveedor ingresado correctamente.");
                _fmrProveedores.MostrarProveedores(_proveedorService.ObtenerTodos());
                this.Close();
            }
            else
            {
                MessageBox.Show("Error en ingresar al proveedor");
            }
        }
    }
}
