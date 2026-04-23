using BusinessLogic;
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

namespace UI.PlanesMembresias
{
    public partial class FmrPlanesRenovar : Form
    {
        private readonly ClienteService _clienteService;
        private readonly PlanMembresiaService _planetMembresiaService;
        private readonly CorteService _corteService;
        private readonly VentaService _ventaService;
        public Usuario Usuario {  get; set; }
        public bool Registro { get; set; }
        public bool Vacio { get; set; }
        public int IdCliente { get; set; } = 0;
        public FmrPlanesRenovar(Usuario usuario)
        {
            InitializeComponent();
            _clienteService = new ClienteService();
            _planetMembresiaService = new PlanMembresiaService();
            _corteService = new CorteService();
            _ventaService = new VentaService();
            cmbMetodo.SelectedIndex = 0;
            this.Usuario = usuario;
        }

        public void Modo()
        {
            if ( Registro)
            {
                CargarDatos(IdCliente);
                btnBuscar.Visible = false;
                txtCodigo.ReadOnly = true;

            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(IdCliente == 0)
            {
                return;
            }
            if (txtCodigo.Text.Length > 0)
            {
                int id = Convert.ToInt32( txtCodigo.Text.Trim());
                CargarDatos(id);
            }

        }

        public void CargarDatos(int id)
        {
            var cliente = _clienteService.ObtenerPorId(IdCliente);
            if (cliente == null) { MessageBox.Show("Error."); return; }
            txtCodigo.Text = id.ToString();
            txtCliente.Text = cliente.Nombre;

            cmbPlanes.DataSource = null;
            cmbPlanes.DataSource = _planetMembresiaService.ObtenerTodos().Select(p => p.Nombre).ToList();
        }

        private void cmbPlanes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var nombrePlan = cmbPlanes.Text.ToLower();
            var resultP = _planetMembresiaService.ObtenerTodos().Find(p => p.Nombre.ToLower().Contains(nombrePlan));

            txtDias.Text = resultP.Duracion_Dias.ToString();
            txtTotal.Text = resultP.Precio.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if(!_corteService.HayCorteAbierto())
            {
                MessageBox.Show("Se necesita abrir un corte para realizar la transacción.");
                return;
            }

            if(Usuario == null)
            {
                MessageBox.Show("Se necesita iniciar sesión para realizar transacción.");
                return;
            }
            var metodo = cmbMetodo.Text;
            var nombrePlan = cmbPlanes.Text.ToLower();
            var resultP = _planetMembresiaService.ObtenerTodos().Find(p => p.Nombre.ToLower().Contains(nombrePlan));
            
            if(resultP == null)
            {
                MessageBox.Show("Se necesita un plan.");
                return;
            }

            var cliente = _clienteService.ObtenerPorId(IdCliente);
            if(cliente == null)
                { return; }

            Venta venta = new Venta()
            {
                Corte_id = _corteService.ObtenerActivo().Corte_Id, 
                Cliente_Id = cliente.Id,
                Usuario_Id = Usuario.Id, 
                Plan_id = resultP.Id,
                Subtotal = resultP.Precio,
                Total = resultP.Precio,
                Metodo_Pago = metodo,
                Tipo_Venta = "membresia"
            };

            var result = _ventaService.Insertar(venta , null);

            if(result > 0)
            {
                MessageBox.Show("Plan pagado exitosamente.");
                this.Close();
            }
        }
    }
}
