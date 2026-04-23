using BusinessLogic;
using BusinessLogic.Utils;
using Entities;
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

namespace UI.Clientes
{
    public partial class FmrClientesDetalles : Form
    {
        private readonly FmrClientes _fmrClientes;
        private readonly ClienteService _clienteService;
        private readonly VisitaService _visitaService;
        private readonly VentaService _ventaService;
        
        public bool Nuevo { get; set; } = false;
        public bool Editar { get; set; } = false;
        public int IdCliente { get; set; } = 0;
        public bool Detalles { get; set; } = false;
        public FmrClientesDetalles(FmrClientes fmrClientes)
        {
            InitializeComponent();
            _fmrClientes = fmrClientes;
            _clienteService = new ClienteService();
            _visitaService = new VisitaService();
            _ventaService = new VentaService();
            Size size = new Size();
            size.Width = 508;
            size.Height = 546;

            this.Size = size;
            tabDatos.Enabled = false;
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
                txtCodigo.Text = IdCliente.ToString();
                CargarDatos();
            }
            else if (Detalles)
            {
                lblEstado.Text = "Modo: Detalles del Registro";
                txtNombre.ReadOnly = true;
                txtApellido.ReadOnly = true;
                txtDocumento.ReadOnly = true;
                txtEmail.ReadOnly = true;
                txtTelefono.ReadOnly = true;
                txtFecha.Enabled = false;
                btnGuardar.Visible = false;
                btnLimpiar.Visible = false;
                cmbActivo.Enabled = false;
                cmbInactivo.Enabled = false;
                Size size = new Size();
                size.Width = 971;
                size.Height = 546;

                this.Size = size;
                tabDatos.Enabled = true;


                CargarDatos();
                CargarVisitas();

            }
        }

        public void CargarDatos()
        {
            if (IdCliente > 0)
            {
                var cliente = _clienteService.ObtenerPorId(IdCliente);
                txtCodigo.Text = IdCliente.ToString();
                txtNombre.Text = cliente.Nombre;
                txtApellido.Text = cliente.Apellido;
                txtEmail.Text = cliente.Email;
                txtTelefono.Text = cliente.Telefono;
                txtDocumento.Text = cliente.Documento;
                txtFecha.Text = cliente.Fecha_Nacimiento.ToString();
                if (cliente.Activo)
                {
                    cmbActivo.Checked = true;
                }
                else
                {
                    cmbInactivo.Checked = true;
                }
            }
        }

        public void CargarVisitas()
        {
            var visitas = _visitaService.ObtenerPorCliente(IdCliente)
                .Select(v => new {Fecha_Ingreso =  v.Fecha_Ingreso.ToString("d") })
                .ToList();
            dataVisitas.DataSource = visitas;
            dataVisitas.Columns["Fecha_Ingreso"].HeaderText = "Fecha de Entrada";
            dataVisitas.Columns["Fecha_Ingreso"].DefaultCellStyle.Format = "dd/MM/yyyy";
            lblRegistrosCompras.Text = $"Registros: {visitas.Count}";
        }

        public void CargarCompras()
        {
            var compras = _ventaService.ObtenerPorCliente(IdCliente)
                .Select(c => new { Fecha = c.Fecha, Producto = c.Concepto, Total = c.Total_Venta, Tipo = c.Linea_Tipo })
                .ToList();

            dataCompras.DataSource = compras;
            lblRegistrosCompras.Text = $"Registros: {compras.Count}";
        }

        private void tabDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 0 - visitas
            // 1 - compras
            int index = tabDatos.SelectedIndex;

            if(index == 0)
            {
                CargarVisitas();
                dataCompras.DataSource = null;
            }
            else
            {
                CargarCompras();
                dataVisitas.DataSource = null;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Nuevo)
            {
                GuardarCliente();
            }
            if (Editar)
            {
                ActualizarCliente();
            }
        }

        private void GuardarCliente()
        {
            var nombre = txtNombre.Text;
            var apellido = txtApellido.Text;
            var email = txtEmail.Text;
            var telefono = txtTelefono.Text;
            var documento = txtDocumento.Text;
            var fecha = txtFecha.Value;
            var activo = true;

            var emailEncontrado = _clienteService.ObtenerTodos().Where( c => c.Email.ToLower().Trim() == email.ToLower().Trim());

            if(emailEncontrado.Any())
            {
                MessageBox.Show("Email ya registrado.");
                return;
            }

            Cliente cliente = new Cliente()
            {
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Telefono = telefono,
                Documento = documento,
                Fecha_Nacimiento = fecha,
                Activo = activo
            };

            var result = _clienteService.Insertar(cliente);

            if (result > 0)
            {
                MessageBox.Show("Cliente ingresado correctamente.");
                _fmrClientes.MostrarClientes(_clienteService.ObtenerTodos());
                this.Close();
            }
            else
            {
                MessageBox.Show("Error en ingresar al cliente");
            }
        }

        private void ActualizarCliente()
        {
            var nombre = txtNombre.Text;
            var apellido = txtApellido.Text;
            var email = txtEmail.Text;
            var telefono = txtTelefono.Text;
            var documento = txtDocumento.Text;
            var fecha = txtFecha.Value;
            var activo = cmbActivo.Checked;

            Cliente cliente = new Cliente()
            {
                Id = IdCliente,
                Nombre = nombre,
                Apellido = apellido,
                Email = email,
                Telefono = telefono,
                Documento = documento,
                Fecha_Nacimiento = fecha,
                Activo = activo
            };

            var result = _clienteService.Actualizar(cliente);

            if (result)
            {
                MessageBox.Show("Cliente actualizado correctamente.");
                _fmrClientes.MostrarClientes(_clienteService.ObtenerTodos());
                this.Close();
            }
            else
            {
                MessageBox.Show("Error en actualizar al cliente");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            txtDocumento.Clear();

            txtFecha.Value = DateTime.Now; // o la fecha que quieras por defecto
            cmbActivo.Checked = false;
        }
    }
}
