using BusinessLogic;
using Entities.VistaModelos;
using Gimnasio.BusinessLogic;
using Gimnasio.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Proveedores;

namespace UI.Productos
{
    public partial class FmrProductos : Form
    {
        private readonly ProductoService _productoService;
        private readonly CategoriaService _categoriaService;

        public FmrProductos()
        {
            InitializeComponent();
            _productoService = new ProductoService();
            _categoriaService = new CategoriaService();
            MostrarProductos(_productoService.ObtenerTodos());
            comboBox1.SelectedIndex = 0;
        }
        public void MostrarProductos(List<ProductoVM> productos)
        {
            try
            {

                var productos_select = productos.Select(p => new { p.Id, p.Nombre, p.Categoria, p.Precio_Venta, p.Precio_Costo, p.Stock_Actual, p.Stock_Minimo, p.Activo }).ToList();
                dataProductos.DataSource = productos_select;
                AgregarColumnaDetalles();
                AgregarColumnaActualizar();
                AgregarColumnaActivar();
                AgregarColumnaEliminar();
                lblRegistros.Text = $"Registros: {productos.Count}";
            }catch(Exception e)
            {

            }
        }

        private void FiltrarData()
        {
            //Nombre
            //Precio Venta
            //Precio Costo
            //Stock
            //Stoc Minimo
            //Activo
            //Categoria

            if (comboBox1.SelectedIndex < 0) return;

            List<ProductoVM> productos = null;
            bool esNumero = decimal.TryParse(txtBuscar.Text, out decimal numero);

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    productos = _productoService.ObtenerTodos()
                        .Where(p => p.Nombre.ToLower().Contains(txtBuscar.Text.ToLower()))
                        .ToList();
                    break;
                case 1:
                    if (esNumero)
                        productos = _productoService.ObtenerTodos()
                            .Where(p => p.Precio_Venta == numero)
                            .ToList();
                    break;
                case 2:
                    if (esNumero)
                        productos = _productoService.ObtenerTodos()
                            .Where(p => p.Precio_Costo == numero)
                            .ToList();
                    break;
                case 3:
                    if (esNumero)
                        productos = _productoService.ObtenerTodos()
                            .Where(p => p.Stock_Actual == Convert.ToInt32(numero))
                            .ToList();
                    break;
                case 4:
                    productos = _productoService.ObtenerTodos()
                        .Where(p => p.Stock_Actual <= p.Stock_Minimo)
                        .ToList();
                    break;
                case 6:
                    productos = _productoService.ObtenerTodos()
                        .Where(p => p.Categoria.ToLower().Contains(txtBuscar.Text.ToLower()))
                        .ToList();
                    break;

            }

            if (productos != null)
                MostrarProductos(productos);
        }
        public void AgregarColumnaEliminar()
        {
            if (dataProductos.Columns.Contains("btnEliminar")) return;

            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "btnEliminar";
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            btnEliminar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnEliminar.DefaultCellStyle.ForeColor = Color.White;

            dataProductos.Columns.Add(btnEliminar);
        }

        public void AgregarColumnaDetalles()
        {
            if (dataProductos.Columns.Contains("btnDetalles")) return;

            DataGridViewButtonColumn btnDetalles = new DataGridViewButtonColumn();
            btnDetalles.Name = "btnDetalles";
            btnDetalles.HeaderText = "Detalles";
            btnDetalles.Text = "Detalles";
            btnDetalles.UseColumnTextForButtonValue = true;
            btnDetalles.DefaultCellStyle.BackColor = Color.IndianRed;
            btnDetalles.DefaultCellStyle.ForeColor = Color.White;

            dataProductos.Columns.Add(btnDetalles);
        }

        private void AgregarColumnaActualizar()
        {
            if (dataProductos.Columns.Contains("btnActualizar")) return;

            DataGridViewButtonColumn btnActualizar = new DataGridViewButtonColumn();
            btnActualizar.Name = "btnActualizar";
            btnActualizar.HeaderText = "Actualizar";
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseColumnTextForButtonValue = true;
            btnActualizar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnActualizar.DefaultCellStyle.ForeColor = Color.White;

            dataProductos.Columns.Add(btnActualizar);
        }

        public void AgregarColumnaActivar()
        {
            if (dataProductos.Columns.Contains("btnActivar")) return;

            DataGridViewButtonColumn btnActivar = new DataGridViewButtonColumn();
            btnActivar.Name = "btnActivar";
            btnActivar.HeaderText = "Activar";
            btnActivar.Text = "Activar";
            btnActivar.UseColumnTextForButtonValue = true;
            btnActivar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnActivar.DefaultCellStyle.ForeColor = Color.White;

            dataProductos.Columns.Add(btnActivar);
        }
        private void dataProveedores_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var row = dataProductos.Rows[e.RowIndex];

            if (row.Cells["Stock_actual"].Value != null)
            {
                int actual = Convert.ToInt32( row.Cells["Stock_Actual"].Value.ToString());
                int minimo = Convert.ToInt32(row.Cells["Stock_Minimo"].Value.ToString());
                if(actual <= minimo)
                {
                    row.DefaultCellStyle.BackColor = Color.Orange;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarData();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            FiltrarData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MostrarProductos(_productoService.ObtenerTodos());
            txtBuscar.Clear();
            comboBox1.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FmrProductosDetalles fmrProductosDetalles = new FmrProductosDetalles(this) { Nuevo = true };
            fmrProductosDetalles.Modo();
            fmrProductosDetalles.ShowDialog();
        }

        private void dataProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int index = e.RowIndex;
            var id = Convert.ToInt32(dataProductos.Rows[index].Cells["Id"].Value);
            string nombre = dataProductos.Rows[index].Cells["Categoria"].Value.ToString();
            int idCategoria = _categoriaService.ObtenerTodos().FirstOrDefault(c => c.Nombre == nombre).Id;
            if (dataProductos.Columns[e.ColumnIndex].Name == "btnActualizar")
            {
                FmrProductosDetalles fmrProductosDetalles = new FmrProductosDetalles(this) { Editar = true, IdProducto = id , IdCategoria = idCategoria};
                fmrProductosDetalles.Modo();
                fmrProductosDetalles.ShowDialog();
            }
            else if (dataProductos.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                EliminarProveedor(id);
            }
            else if (dataProductos.Columns[e.ColumnIndex].Name == "btnActivar")
            {
                ActivarProducto(id);
            }
            else if (dataProductos.Columns[e.ColumnIndex].Name == "btnDetalles")
            {
                FmrProductosDetalles fmrProductosDetalles = new FmrProductosDetalles(this) { Detalles = true, IdProducto = id, IdCategoria = idCategoria };
                fmrProductosDetalles.Modo();
                fmrProductosDetalles.ShowDialog();
            }
        }

        private void ActivarProducto(int id)
        {
            var confirmResult = MessageBox.Show("¿Está seguro de activar este Producto?", "Confirmar activación", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _productoService.Activar(id);
                MostrarProductos(_productoService.ObtenerTodos());
            }
        }

        private void EliminarProveedor(int id)
        {
            var confirmResult = MessageBox.Show("¿Está seguro de eliminar este producto?", "Confirmar eliminación", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _productoService.Eliminar(id);
                MostrarProductos(_productoService.ObtenerTodos());
            }
        }
    }
}
