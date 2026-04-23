using BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Compras
{
    public partial class FmrCompraDetalles : Form
    {
        private readonly CompraService _compraService;
        public int IdCompra { get; set; }

        public FmrCompraDetalles()
        {
            InitializeComponent();
            _compraService = new CompraService();
        }

        public void CargarDatos()
        {
            var compraDetalle = _compraService.ObtenerPorIdVWDetalles(IdCompra);
            var compra = _compraService.ObtenerPorId(IdCompra);
            txtCodigo.Text = IdCompra.ToString();
            txtUsuario.Text = compra.Usuario;
            txtTotal.Text = compra.Total.ToString();
            txtFecha.Text = compra.Fecha.ToString("G");
            txtEstado.Text = compra.Estado;

            var detalles = compraDetalle.Select(d => new
            {
                Proveedor = d.Proveedor,
                Producto = d.Producto,
                Precio_Unitario = d.Precio_Unitario,
                Cantidad = d.Cantidad,
                Subtotal = d.Subtotal
            })
                .ToList();

            dataCompras.DataSource = detalles;
            lblItems.Text = $"Items: {detalles.Count}";
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
