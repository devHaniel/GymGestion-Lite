using BusinessLogic;
using Entities.VistaModelos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Ventas;

namespace UI.Productos
{
    public partial class FmrProductoVenta : Form
    {
        private readonly ProductoService productoServicio = new ProductoService();
        private List<ProductoVM> _todosLosProductos;
        private readonly FmrVentasRealizar fmrVentas;
        public FmrProductoVenta(FmrVentasRealizar fmrVentasRealizar)
        {
            InitializeComponent();
            fmrVentas = fmrVentasRealizar;
            Mostrar();
        }
        private void Mostrar()
        {
            _todosLosProductos = productoServicio.ObtenerTodos();
            dataProductos.DataSource = _todosLosProductos;
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            var filtro = txtNombre.Text.Trim().ToLower();
            dataProductos.DataSource = string.IsNullOrEmpty(filtro)
                ? _todosLosProductos
                : _todosLosProductos
                    .Where(p => p.Nombre.ToLower().Contains(filtro))
                    .ToList();
        }

        private void dataProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var id = (int)dataProductos.Rows[e.RowIndex].Cells[0].Value;

            // ✅ Solo agregar si no existe ya en la lista
            if (!fmrVentas.Productos.Any(p => p.Id == id))
            {
                fmrVentas.Productos.Add(productoServicio.ObtenerPorId(id));
            }

            // Apuntar al índice del producto (ya exista o sea nuevo)
            fmrVentas.IndexProducto = fmrVentas.Productos.FindIndex(p => p.Id == id);

            this.Close(); 
        }
    }
}
