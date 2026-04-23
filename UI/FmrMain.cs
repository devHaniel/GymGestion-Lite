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
using UI.Categorias;
using UI.Clientes;
using UI.Compras;
using UI.Cortes;
using UI.PlanesMembresias;
using UI.Productos;
using UI.Proveedores;
using UI.Ventas;

namespace UI
{
    public partial class FmrMain : Form
    {
        public Usuario Usuario { get; set; }
        private readonly CorteService corteService = new CorteService();
        public FmrMain(Usuario usuario)
        {
            InitializeComponent();
            this.Usuario = usuario;
            toolStripStatusLabelHora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            statusUsuario.Text = $"Usuario: {Usuario.Nombre} | ";
            CorteActivo();
        }

        public void CorteActivo()
        {
            if (corteService.HayCorteAbierto())
            {
                statusCaja.Text = "Caja: Abierta | ";
            }
            else
            {
                statusCaja.Text = "Caja: Cerrada | ";

            }
        }

        private void AbrirForm(Form formHijo)
        {
            panelVistas.Controls.Clear();

            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.None; // IMPORTANTE

            panelVistas.Controls.Add(formHijo);
            formHijo.Show();
            formHijo.Location = new Point(
            (panelVistas.Width - formHijo.Width) / 2,
            (panelVistas.Height - formHijo.Height) / 2
            );
        }

        private void panelVistas_Resize(object sender, EventArgs e)
        {
            if (panelVistas.Controls.Count > 0)
            {
                var form = panelVistas.Controls[0];

                form.Location = new Point(
                    (panelVistas.Width - form.Width) / 2,
                    (panelVistas.Height - form.Height) / 2
                );
            }
        }
        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            FmrCorteMain fmrCorteMain = new FmrCorteMain(this.Usuario);
            lblTitulo.Text = "Cortes";
            AbrirForm(fmrCorteMain);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            FmrClientes fmrClientes = new FmrClientes();
            lblTitulo.Text = "Clientes";

            AbrirForm(fmrClientes);
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            FmrProductos fmrProductos = new FmrProductos();
            lblTitulo.Text = "Productos";

            AbrirForm(fmrProductos);
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            FmrCompras fmrCompras = new FmrCompras(Usuario);
            lblTitulo.Text = "Compras";

            AbrirForm(fmrCompras);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FmrPlanesMembresias fmrPlanesMembresias = new FmrPlanesMembresias();
            lblTitulo.Text = "Membresias";

            AbrirForm(fmrPlanesMembresias);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FmrCategorias fmrCategorias = new FmrCategorias();
            lblTitulo.Text = "Categorias";

            AbrirForm(fmrCategorias);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabelHora.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            CorteActivo();
        }

        private void btnOperaciones_Click(object sender, EventArgs e)
        {
            FmrOperaciones fmrOperaciones = new FmrOperaciones(Usuario);
            lblTitulo.Text = "Operaciones";
            AbrirForm(fmrOperaciones);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FmrProveedores fmrProveedores = new FmrProveedores();
            lblTitulo.Text = "Proveedores";
            AbrirForm(fmrProveedores);
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            FmrVentas fmrVentas = new FmrVentas(Usuario);
            lblTitulo.Text = "Ventas";
            AbrirForm(fmrVentas);
        }

        private void btnDatos_Click(object sender, EventArgs e)
        {
            FmrDatabaseOperaciones fmrDatabaseOperaciones = new FmrDatabaseOperaciones();
            lblTitulo.Text = "Datos";
            AbrirForm(fmrDatabaseOperaciones);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            FmrUsuarios usuarios = new FmrUsuarios();
            lblTitulo.Text = "Usuarios";
            AbrirForm(usuarios);
        }
    }
}
