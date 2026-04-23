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
using UI.Compras;
using VentaProductos.utilidades;

namespace VentaProductos
{
    public partial class FmrProductosCompra : Form
    {
        private readonly ProductoService productoServicio = new ProductoService();
        private readonly FmrComprasRealizar fmrCompra;
        public FmrProductosCompra(FmrComprasRealizar fmrCompra)
        {
            InitializeComponent();
            this.fmrCompra = fmrCompra;
            this.Mostrar();
        }

        private void Mostrar()
        {
            dataProductos.DataSource = productoServicio.ObtenerTodos();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            dataProductos.DataSource = productoServicio.ObtenerTodos().Where(p => p.Nombre.ToLower().Contains(txtNombre.Text.ToLower())).ToList();
        }

        private void dataProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            this.fmrCompra.Productos.Add( productoServicio.ObtenerPorId((int)dataProductos.Rows[e.RowIndex].Cells[0].Value));
            this.fmrCompra.IndexProducto = this.fmrCompra.Productos.Count - 1;
            this.Close();
        }
    }
}
