using BusinessLogic;
using Entities.VistaModelos;
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
using UI.Compras;

namespace UI.Ventas
{
    public partial class FmrVentas : Form
    {
        private readonly VentaService _ventaService;
        public Usuario Usuario {  get; set; }
        public FmrVentas(Usuario usuario)
        {
            InitializeComponent();
            _ventaService = new VentaService();
            MostrarVentas(_ventaService.ObtenerTodas());
            this.Usuario = usuario;
            comboBox1.SelectedIndex = 0;
        }

        public void MostrarVentas(List<VentasVM> ventas)
        {
            var resultado = ventas.Select(v => new
            {
                Id = v.Id,
                Cliente = v.Cliente,
                Producto = v.Tipo_venta,
                Total = v.Total,
                Usuario = v.Usuario,
                Fecha = v.Fecha

            })
            .ToList();
            dataVentas.DataSource = resultado;
            AgregarColumnaDetalles();
            lblRegistros.Text = $"Registros: {ventas.Count}";
        }

        public void AgregarColumnaDetalles()
        {
            if (dataVentas.Columns.Contains("btnDetalles")) return;

            DataGridViewButtonColumn btnDetalles = new DataGridViewButtonColumn();
            btnDetalles.Name = "btnDetalles";
            btnDetalles.HeaderText = "Detalles";
            btnDetalles.Text = "Detalles";
            btnDetalles.UseColumnTextForButtonValue = true;
            btnDetalles.DefaultCellStyle.BackColor = Color.IndianRed;
            btnDetalles.DefaultCellStyle.ForeColor = Color.White;

            dataVentas.Columns.Add(btnDetalles);
        }

        private void Filtrar()
        {
            var ventas = _ventaService.ObtenerTodas();

            // Filtrar por estado
            string venta = comboBox1.Text.ToLower().Trim();

            if(venta != "todos")
            {
                ventas = ventas
                    .Where(c => c.Tipo_venta.ToLower().Trim() == venta)
                    .ToList();
            }
            // Filtrar por fecha
            DateTime fecha = txtFecha.Value.Date;

            ventas = ventas
                .Where(c => c.Fecha.Date == fecha)
                .ToList();

            MostrarVentas(ventas);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fecha = txtFecha.Value.Date;
            MostrarVentas(_ventaService.ObtenerTodas().Where(c => c.Fecha.Date == fecha).ToList());
        }

        private void txtFecha_ValueChanged(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void dataVentas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex < 0) return;
                int index = e.RowIndex;
                var id = Convert.ToInt32(dataVentas.Rows[index].Cells["Id"].Value);
                if (dataVentas.Columns[e.ColumnIndex].Name == "btnDetalles")
                {
                    FmrVentasDetalles fmrVentasDetalles = new FmrVentasDetalles() { IdVenta = id };
                    fmrVentasDetalles.CargarDatos();
                    fmrVentasDetalles.ShowDialog();
                }
            }
            catch(Exception a)
            {
                Console.WriteLine(a.Message);
            }
        }
    }
}
