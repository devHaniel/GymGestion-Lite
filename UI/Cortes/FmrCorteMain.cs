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

namespace UI.Cortes
{
    public partial class FmrCorteMain : Form
    {
        private readonly CorteService _corteService;
        public Usuario Usuario {  get; set; }
        public FmrCorteMain(Usuario usuario)
        {
            InitializeComponent();
            this.Usuario = usuario;
            _corteService = new CorteService();
            CargarCortes();
        }

        private void CargarCortes()
        {
            var cortes = _corteService.ObtenerHistorial()
                .Select(c => new
                {
                    c.Id,
                    c.Fecha_Apertura,
                    c.Fecha_Cierre,
                    c.Nombre_Cliente,
                    c.Total_Ventas,
                    c.Total_Compras,
                    c.Gran_Total,
                    c.Estado
                })
                .ToList();

            dataCortes.DataSource = cortes;
            dataCortes.Columns["Fecha_Apertura"].HeaderText = "Fecha Apertura";
            dataCortes.Columns["Fecha_Cierre"].HeaderText = "Fecha Cierre";
            dataCortes.Columns["Nombre_Cliente"].HeaderText = "Cliente";
            dataCortes.Columns["Total_Ventas"].HeaderText = "Ventas";
            dataCortes.Columns["Total_Compras"].HeaderText = "Compras";
            dataCortes.Columns["Gran_Total"].HeaderText = "Total General";
            dataCortes.Columns["Estado"].HeaderText = "Estado";
        }

        private void CargarCortesFiltrados()
        {
            DateTime desde = dtDesde.Value.Date;
            DateTime hasta = dtHasta.Value.Date.AddDays(1);

            var cortes = _corteService.ObtenerHistorial()
                .Where(c =>
                    c.Fecha_Apertura >= desde &&
                    c.Fecha_Apertura < hasta
                )
                .Select(c => new
                {
                    c.Id,
                    c.Fecha_Apertura,
                    c.Fecha_Cierre,
                    c.Nombre_Cliente,
                    c.Total_Ventas,
                    c.Total_Compras,
                    c.Gran_Total,
                    c.Estado
                })
                .ToList();

            dataCortes.DataSource = cortes;
            dataCortes.Columns["Fecha_Apertura"].HeaderText = "Fecha Apertura";
            dataCortes.Columns["Fecha_Cierre"].HeaderText = "Fecha Cierre";
            dataCortes.Columns["Nombre_Cliente"].HeaderText = "Cliente";
            dataCortes.Columns["Total_Ventas"].HeaderText = "Ventas";
            dataCortes.Columns["Total_Compras"].HeaderText = "Compras";
            dataCortes.Columns["Gran_Total"].HeaderText = "Total General";
            dataCortes.Columns["Estado"].HeaderText = "Estado";
        }

        private void MostrarDatos(Corte corte)
        {
            ///*txtDesdeF*/.Text = corte.Fecha_Apertura.ToString();
            //txtHastaF.Text = corte.Fecha_Cierre.ToString();

            lblNCorte.Text = corte.Id.ToString();
            // General
            txtCajero.Text = corte.Nombre_Cliente;
            txtApertura.Text = corte.Fecha_Apertura.ToString();
            txtCierre.Text = corte.Fecha_Cierre.ToString();
            txtMontoI.Text = corte.Monto_Inicial.ToString();
            txtTotalC.Text = corte.Total_Compras.ToString();
            txtGTotal.Text = corte.Gran_Total.ToString();

            // métodos de pago
            txtEfectivoM.Text = corte.Total_Efectivo.ToString();
            txtTarjetaM.Text = corte.Total_Tarjeta.ToString();
            txtTransferenciaM.Text = corte.Total_Transferencia.ToString();
            txtTotalM.Text = corte.Total_Ventas.ToString();

            txtMembresia.Text = corte.Total_Membresias.ToString();
            txtProducto.Text = corte.Total_Productos.ToString();
        }

        private void dataCortes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            { return; }

            try
            {
                int id = Convert.ToInt32(dataCortes.Rows[e.RowIndex].Cells["Id"].Value);
                var resultado = _corteService.ObtenerPorId(id);
                MostrarDatos(resultado);
            }
            catch (Exception)
            {

            }
        }

        private void dtDesde_ValueChanged(object sender, EventArgs e)
        {
            CargarCortesFiltrados();
        }

        private void dtHasta_ValueChanged(object sender, EventArgs e)
        {
            CargarCortesFiltrados();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime filtro = dtFiltro.Value.Date;

            var cortes = _corteService.ObtenerHistorial()
                .Where(c =>
                    c.Fecha_Apertura >= filtro
                )
                .Select(c => new
                {
                    c.Id,
                    c.Fecha_Apertura,
                    c.Fecha_Cierre,
                    c.Nombre_Cliente,
                    c.Total_Ventas,
                    c.Total_Compras,
                    c.Gran_Total,
                    c.Estado
                })
                .ToList();

            dataCortes.DataSource = cortes;
            dataCortes.Columns["Fecha_Apertura"].HeaderText = "Fecha Apertura";
            dataCortes.Columns["Fecha_Cierre"].HeaderText = "Fecha Cierre";
            dataCortes.Columns["Nombre_Cliente"].HeaderText = "Cliente";
            dataCortes.Columns["Total_Ventas"].HeaderText = "Ventas";
            dataCortes.Columns["Total_Compras"].HeaderText = "Compras";
            dataCortes.Columns["Gran_Total"].HeaderText = "Total General";
            dataCortes.Columns["Estado"].HeaderText = "Estado";
        }

        private void btnRefrescarC_Click(object sender, EventArgs e)
        {
            CargarCortes();
            lblNCorte.Text = "0";
            dtFiltro.ResetText();
            dtHasta.ResetText();
            dtDesde.ResetText();
        }

        private void dataCortes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var col = dataCortes.Columns[e.ColumnIndex];
            var row = dataCortes.Rows[e.RowIndex];

            if (col.DataPropertyName == "Total_Ventas")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal ventas))
                {
                    e.CellStyle.ForeColor = Color.Green;
                    e.CellStyle.Font = new Font(dataCortes.Font, FontStyle.Bold);
                }
            }

            if (col.DataPropertyName == "Total_Compras")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal compras))
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(dataCortes.Font, FontStyle.Bold);
                }
            }

            if (col.DataPropertyName == "Gran_Total")
            {
                if (e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal total))
                {
                    if (total > 0)
                    {
                        e.CellStyle.ForeColor = Color.Green; // ganancia
                        row.DefaultCellStyle.BackColor = Color.Honeydew;
                    }
                    else if (total < 0)
                    {
                        e.CellStyle.ForeColor = Color.Red;   // pérdida
                        row.DefaultCellStyle.BackColor = Color.MistyRose;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Orange;
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                }

            }
            if (col.DataPropertyName == "Estado")
            {
                if (e.Value != null)
                {
                    string estado = e.Value.ToString();

                    if (estado.Equals("Abierto", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.Font = new Font(dataCortes.Font, FontStyle.Bold);
                    }
                    else if (estado.Equals("Cerrado", StringComparison.OrdinalIgnoreCase))
                    {
                        e.CellStyle.ForeColor = Color.Orange;
                        e.CellStyle.Font = new Font(dataCortes.Font, FontStyle.Bold);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(_corteService.HayCorteAbierto())
            {
                MessageBox.Show("Ya existe un corte abierto. Cierralo para abrir otro.");
                return;
            }
            if (Usuario != null)
            {
                FmrCorteAbrir fmrCorteAbrir = new FmrCorteAbrir(this.Usuario);
                fmrCorteAbrir.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(_corteService.HayCorteAbierto())
            {
                FmrCorte fmrCorte = new FmrCorte();
                fmrCorte.ShowDialog();
            }
        }
    }
}
