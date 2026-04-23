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

namespace VentaProductos.utilidades
{
    public partial class FmrProveedoresCompra : Form
    {
        private readonly FmrComprasRealizar fmrCompra;
        private readonly ProveedorService _proveedorServicio;

        public FmrProveedoresCompra(FmrComprasRealizar fmrCompra)
        {
            InitializeComponent();
            this.fmrCompra = fmrCompra;
            _proveedorServicio = new ProveedorService();
            this.Mostrar();
        }

        private void Mostrar()
        {
            dataProveedores.DataSource = _proveedorServicio.ObtenerTodos();
        }

        private void dataProveedores_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;

            this.fmrCompra.Proveedor = _proveedorServicio.ObtenerPorId((int)dataProveedores.Rows[e.RowIndex].Cells[0].Value);
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataProveedores.DataSource = _proveedorServicio.ObtenerTodos().Where(p => p.Nombre.ToLower().Contains(txtNombre.Text.ToLower())).ToList();
        }
    }
}
