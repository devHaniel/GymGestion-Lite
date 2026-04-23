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
using VentaProductos;
using VentaProductos.utilidades;

namespace UI.Compras
{
    public partial class FmrComprasRealizar : Form
    {
        private readonly CompraService _compraService;
        private readonly CorteService _corteService;
        public Proveedor Proveedor { get; set; }
        public List<ProductoVM> Productos { get; set; }
        public int IndexProducto { get; set; }
        public List<DetalleCompra> Detalles { get; set; }
        private decimal Total { get { return Detalles.Sum(d => d.Subtotal); } }
        public Usuario Usuario {  get; set; }

        public FmrComprasRealizar(Usuario usuario)
        {
            InitializeComponent();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            Productos = new List<ProductoVM>();
            Detalles = new List<DetalleCompra>();
            _compraService = new CompraService();
            _corteService = new CorteService();
            this.Usuario = usuario;
            txtCantidad.Value = 1;
        }

        public void Mostrar()
        {

            dataCompra.DataSource = Detalles
        .Select(d => {
            var producto = Productos.FirstOrDefault(p => p.Id == d.Producto_Id);

            return new
            {
                Id = d.Producto_Id,
                Nombre = producto != null ? producto.Nombre : "N/A",
                Cantidad = d.Cantidad,
                Precio = d.Precio_Unitario,
                Subtotal = d.Cantidad * d.Precio_Unitario
            };
        })
        .ToList();
            AgregarColumnaEliminar();
        }

        private void AgregarColumnaEliminar()
        {
            if (dataCompra.Columns.Contains("btnEliminar")) return;

            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "btnEliminar";
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            btnEliminar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnEliminar.DefaultCellStyle.ForeColor = Color.White;

            dataCompra.Columns.Add(btnEliminar);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FmrProveedoresCompra fmrProveedoresCompra = new FmrProveedoresCompra(this);
            fmrProveedoresCompra.ShowDialog();

            if (Proveedor != null)
            {
                txtProveedor.Text = Proveedor.Nombre;
                txtCodigoProveedor.Text = Proveedor.Id.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FmrProductosCompra fmrProductosCompra = new FmrProductosCompra(this);
            fmrProductosCompra.ShowDialog();

            try
            {
                if (Productos != null)
                {
                    var producto = Productos[IndexProducto];
                    txtNProducto.Text = producto.Id.ToString();
                    txtProducto.Text = producto.Nombre;
                    txtPrecio.Text = producto.Precio_Costo.ToString("0.00");
                    txtStock.Text = producto.Stock_Actual.ToString();

                }
            }catch(Exception)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Productos.Count == 0) return;

            var productoSeleccionado = Productos[IndexProducto];
            if (IndexProducto < 0 || IndexProducto >= Productos.Count)
            {
                MessageBox.Show("Seleccione un producto");
                return;
            }
            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Cantidad inválida");
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("Precio inválido");
                return;
            }
            var detalleExistente = Detalles
                .FirstOrDefault(d => d.Producto_Id == productoSeleccionado.Id);

            if (detalleExistente != null)
            {
                detalleExistente.Cantidad += cantidad;
            }
            else
            {
                Detalles.Add(new DetalleCompra
                {
                    Producto_Id = productoSeleccionado.Id,
                    Cantidad = cantidad,
                    Precio_Unitario = precio
                });
            }

            Mostrar();
            txtTotal.Text = Total.ToString("0.00");
        }

        private void dataCompra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataCompra.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                int index = e.RowIndex;
                int productoId = (int)dataCompra.Rows[e.RowIndex].Cells["Id"].Value;

                var detalle = Detalles.FirstOrDefault(d => d.Producto_Id == productoId);

                if (detalle != null)
                {
                    detalle.Cantidad -= 1;

                    if (detalle.Cantidad <= 0)
                        Detalles.Remove(detalle);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var corteActivo = _corteService.HayCorteAbierto();
            if (!corteActivo)
            {
                MessageBox.Show("Para realizar una compra necesita un corte activo.");
                return;
            }


            if (Proveedor == null)
            {
                MessageBox.Show("Seleccione un proveedor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Detalles.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var compra = new Compra
            {
                Fecha = DateTime.Now,
                Total = Total,
                Usuario_Id = Usuario.Id,
                Proveedor_Id = Proveedor.Id,
                Corte_Id = _corteService.ObtenerActivo().Corte_Id
            };



            int resultado = _compraService.Insertar(compra, Detalles);

            if (resultado > 0)
            {
                MessageBox.Show("Compra registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Limpiar formulario
                Detalles.Clear();
                Productos.Clear();
                Proveedor = null;
                LimpiarFormulario();
                this.Mostrar();
            }
            else
            {
                MessageBox.Show("Error al registrar la compra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            // 🔹 Limpiar TextBox
            txtProveedor.Clear();
            txtCodigoProveedor.Clear();
            txtNProducto.Clear();
            txtProducto.Clear();
            txtPrecio.Clear();
            txtCantidad.Value = 1;
            txtStock.Clear();
            txtTotal.Text = "0.00";

            // 🔹 Limpiar listas
            Detalles.Clear();
            Productos.Clear();

            // 🔹 Resetear objetos
            Proveedor = null;
            IndexProducto = -1;

            // 🔹 Limpiar DataGridView
            dataCompra.DataSource = null;
            dataCompra.Rows.Clear();
            dataCompra.Columns.Clear(); // importante si agregas columnas dinámicas
            txtCodigoProveedor.Focus();
            // 🔹 Volver a estructura inicial
            Mostrar();
        }
    }
}
