using BusinessLogic;
using BusinessLogic.Utils;
using Gimnasio.BusinessLogic;
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
using UI.Proveedores;

namespace UI.Productos
{
    public partial class FmrProductosDetalles : Form
    {
        private readonly CategoriaService _categoriaService;
        private readonly ProductoService _productoService;
        private readonly FmrProductos _fmrProductos;
        public bool Nuevo { get; set; } = false;
        public bool Editar { get; set; } = false;
        public int IdProducto { get; set; } = 0;
        public int IdCategoria { get; set; }
        public bool Detalles { get; set; } = false;
        public FmrProductosDetalles(FmrProductos fmrProductos)
        {
            InitializeComponent();
            _categoriaService = new CategoriaService();
            _productoService = new ProductoService();
            _fmrProductos = fmrProductos;
            LlenarCmb();
        }

        private void LlenarCmb()
        {
            cmbCategoria.DataSource = null;
            cmbCategoria.DataSource = _categoriaService.ObtenerTodos().Select(c => c.Nombre).ToList();
        }
        public void Modo()
        {
            if (Nuevo)
            {
                lblEstado.Text = "Modo: Nuevo Registro";
                txtCodigo.Text = "--";
                txtNombre.Focus();
            }
            else if (Editar)
            {
                lblEstado.Text = "Modo: Editar Registro";
                txtCodigo.Text = IdProducto.ToString();
                CargarDatos();
            }
            else if (Detalles)
            {
                lblEstado.Text = "Modo: Detalles del Registro";
                txtNombre.ReadOnly = true;
                txtPrecioCompra.ReadOnly = true;
                txtPrecioVenta.ReadOnly = true;
                txtStockActual.ReadOnly = true;
                txtStockMin.ReadOnly = true;
                cmbCategoria.Enabled = false;
                btnGuardar.Visible = false;
                btnLimpiar.Visible = false;
                CargarDatos();

            }
        }

        public void CargarDatos()
        {
            if (IdProducto > 0 && IdCategoria > 0)
            {
                var producto = _productoService.ObtenerPorId(IdProducto);
                var categoria = _categoriaService.ObtenerPorId(IdCategoria);

                txtCodigo.Text = producto.Id.ToString();
                txtNombre.Text = producto.Nombre;
                txtPrecioVenta.Text = producto.Precio_Venta.ToString();
                txtPrecioCompra.Text = producto.Precio_Costo.ToString();
                txtStockActual.Text = producto.Stock_Actual.ToString();
                txtStockMin.Text = producto.Stock_Minimo.ToString();
                cmbCategoria.Text = categoria.Nombre;
                if (producto.Activo)
                {
                    cmbActivo.Checked = true;
                }
                else
                {
                    cmbInactivo.Checked = true;
                }
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Nuevo)
            {
                GuardarProductos();
            }
            if (Editar)
            {
                ActualizarProductos();
            }
        }

        private void ActualizarProductos()
        {
            var nombre = txtNombre.Text;
            var precio_venta = Convert.ToDecimal(txtPrecioVenta.Text);
            var precio_compra = Convert.ToDecimal(txtPrecioCompra.Text);
            var stock_actual = Convert.ToInt16(txtStockActual.Value);
            var stock_minimo = Convert.ToInt16(txtStockMin.Value);
            var categoria = _categoriaService.ObtenerPorId(IdCategoria).Id;

            Producto producto = new Producto()
            {
                Id = IdProducto,
                Nombre = nombre,
                Precio_Venta = precio_venta,
                Precio_Costo = precio_compra,
                Stock_Actual = stock_actual,
                Stock_Minimo = stock_minimo,
                Categoria_Id = categoria
            };

            var result = _productoService.Actualizar(producto);
            if (result)
            {
                MessageBox.Show("Producto actualizado correctamente.");
                _fmrProductos.MostrarProductos(_productoService.ObtenerTodos());
                this.Close();
            }
            else
            {
                MessageBox.Show("Error en actualizar al producto");
            }
        }

        private void GuardarProductos()
        {
            try
            {

                var nombre = txtNombre.Text;
                var precio_venta = Convert.ToDecimal(txtPrecioVenta.Text);
                var precio_compra = Convert.ToDecimal(txtPrecioCompra.Text);
                var stock_actual =  Convert.ToInt16(txtStockActual.Value);
                var stock_minimo = Convert.ToInt16(txtStockMin.Value);
                var activo = cmbActivo.Checked;
                var categoria = _categoriaService.ObtenerPorId(IdCategoria).Id;

                Producto producto = new Producto()
                {
                    Nombre = nombre,
                    Precio_Venta = precio_venta,
                    Precio_Costo = precio_compra,
                    Stock_Actual = stock_actual,
                    Stock_Minimo = stock_minimo,
                    Activo = activo,
                    Categoria_Id = categoria
                };

                var result = _productoService.Insertar(producto);

                if (result > 0)
                {
                    MessageBox.Show("Producto ingresado correctamente.");
                    _fmrProductos.MostrarProductos(_productoService.ObtenerTodos());
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error en ingresar el producto");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
