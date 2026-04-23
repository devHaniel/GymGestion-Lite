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

namespace UI.Ventas
{
    public partial class FmrVentasDetalles : Form
    {
        private readonly VentaService _ventaService;
        public int IdVenta { get; set; }
        public FmrVentasDetalles()
        {
            InitializeComponent();
            _ventaService = new VentaService();
        }

        public void CargarDatos()
        {
            var ventadDetalle = _ventaService.ObtenerPorIdVWDetalles(IdVenta);
            var venta = _ventaService.ObtenerPorId(IdVenta);
            txtCodigo.Text = IdVenta.ToString();
            txtUsuario.Text = venta.Usuario;
            txtTotal.Text = venta.Total.ToString();
            txtFecha.Text = venta.Fecha.ToString("G");

            var detalles = ventadDetalle.Select(d => new
            {
                Producto = d.Concepto,
                Precio_Unitario = d.Precio_Unitario,
                Cantidad = d.Cantidad,
                Subtotal = d.Subtotal,
                Tipo = d.Linea_Tipo
            })
                .ToList();

            dataVentas.DataSource = detalles;
            lblItems.Text = $"Items: {detalles.Count}";
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
