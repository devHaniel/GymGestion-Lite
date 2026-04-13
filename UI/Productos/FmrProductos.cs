using BusinessLogic;
using Entities.VistaModelos;
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

namespace UI.Productos
{
    public partial class FmrProductos : Form
    {
        private readonly ProductoService _productoService;

        public FmrProductos()
        {
            InitializeComponent();
            _productoService = new ProductoService();
            MostrarProductos(_productoService.ObtenerTodos());
            comboBox1.SelectedIndex = 0;
        }
        public void MostrarProductos(List<ProductoVM> productos)
        {
            try
            {

                var productos_select = productos.Select(p => new { p.Id, p.Nombre, p.Categoria, p.Precio_Venta, p.Precio_Costo, p.Stock_Actual, p.Stock_Minimo, p.Activo }).ToList();
                dataProveedores.DataSource = productos_select;
                AgregarColumnaDetalles();
                AgregarColumnaActualizar();
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
            if (dataProveedores.Columns.Contains("btnEliminar")) return;

            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "btnEliminar";
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            btnEliminar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnEliminar.DefaultCellStyle.ForeColor = Color.White;

            dataProveedores.Columns.Add(btnEliminar);
        }

        public void AgregarColumnaDetalles()
        {
            if (dataProveedores.Columns.Contains("btnDetalles")) return;

            DataGridViewButtonColumn btnDetalles = new DataGridViewButtonColumn();
            btnDetalles.Name = "btnDetalles";
            btnDetalles.HeaderText = "Detalles";
            btnDetalles.Text = "Detalles";
            btnDetalles.UseColumnTextForButtonValue = true;
            btnDetalles.DefaultCellStyle.BackColor = Color.IndianRed;
            btnDetalles.DefaultCellStyle.ForeColor = Color.White;

            dataProveedores.Columns.Add(btnDetalles);
        }

        private void AgregarColumnaActualizar()
        {
            if (dataProveedores.Columns.Contains("btnActualizar")) return;

            DataGridViewButtonColumn btnActualizar = new DataGridViewButtonColumn();
            btnActualizar.Name = "btnActualizar";
            btnActualizar.HeaderText = "Actualizar";
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseColumnTextForButtonValue = true;
            btnActualizar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnActualizar.DefaultCellStyle.ForeColor = Color.White;

            dataProveedores.Columns.Add(btnActualizar);
        }

        private void dataProveedores_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var row = dataProveedores.Rows[e.RowIndex];

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
    }
}
