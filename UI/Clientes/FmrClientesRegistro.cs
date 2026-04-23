using BusinessLogic;
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
using UI.PlanesMembresias;

namespace UI.Clientes
{
    public partial class FmrClientesRegistro : Form
    {
        private readonly MembresiaService _membresiaService;
        private readonly ClienteService _clienteService;
        private readonly VisitaService _visitaService;
        private int Id = 0;
        public Usuario Usuario {  get; set; }
        public FmrClientesRegistro(Usuario usuario)
        {
            InitializeComponent();
            _clienteService = new ClienteService();
            _membresiaService = new MembresiaService();
            _visitaService = new VisitaService();
            this.Usuario = usuario;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Cliente cliente = null;
            if (txtCodigo.Text.Length > 0)
            {
                Id = Convert.ToInt16(txtCodigo.Text);
                cliente = _clienteService.ObtenerPorId(Id);
                CargarDatos(cliente);
            }
            
            if(cliente != null)
            {
                _visitaService.RegistrarVisita(Id);
            }
        }

        private void CargarDatos(Cliente cliente)
        {
            if (cliente == null) { MessageBox.Show("Error."); return; }
            var clienteEncontrado = _membresiaService.ObtenerActivaPorCliente(cliente.Id);
            txtCliente.Text = cliente.Nombre;

            if (clienteEncontrado == null)
            {
                MessageBox.Show("El cliente no posee membresía activa.");
                txtMembresia.Text = "Ninguna";
                lblDias.Text = "0 días";
                lblVence.Text = "Vence el: 00/00/0000";
                button1.Visible = true;
                return;
            }
            try
            {
                
                txtMembresia.Text = clienteEncontrado.Plan_n;
                lblDias.Text = $"{clienteEncontrado.Dias_Restantes} días";
                lblVence.Text = $"Vence el: {clienteEncontrado.Fecha_Fin.ToString("d")}";
                button1.Visible = false;


            }
            catch (Exception e)
            {
                MessageBox.Show($"No posee membresia: {e.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FmrPlanesRenovar fmrPlanesRenovar = new FmrPlanesRenovar(Usuario) { IdCliente = Id, Registro = true};
            fmrPlanesRenovar.Modo();
            fmrPlanesRenovar.ShowDialog();
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
