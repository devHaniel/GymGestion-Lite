using BusinessLogic;
using Entities.VistaModelos;
using Gimnasio.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UI.Productos;

namespace UI.Ventas
{
    public partial class FmrVentasRealizar : Form
    {
        private readonly CorteService _corteService;
        private readonly VentaService _ventaService;
        private readonly ClienteService _clienteService;
        public List<ProductoVM> Productos { get; set; }
        public List<DetalleVenta> Detalles { get; set; }
        public int IndexProducto { get; set; }
        public Usuario Usuario { get; set; }
        private decimal Total {  get; set; }
        private Cliente Cliente { get; set; }

        public FmrVentasRealizar(Usuario usuario)
        {
            InitializeComponent();
            _corteService = new CorteService();
            _ventaService = new VentaService();
            _clienteService = new ClienteService();

            Productos = new List<ProductoVM>();
            Detalles = new List<DetalleVenta>();

            AgregarColumnaEliminar();
            Usuario = usuario;

            dataVenta.CellClick += DataVenta_CellClick;
            cmbMetodo.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fmrProductoVenta = new FmrProductoVenta(this);
            fmrProductoVenta.ShowDialog();

            if (IndiceValido())
            {
                txtProducto.Text = Productos[IndexProducto].Nombre;
                Mostrar();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!IndiceValido())
            {
                MessageBox.Show("Seleccione un producto válido.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productoSeleccionado = Productos[IndexProducto];

            // Verificar stock disponible
            var detalleExistente = Detalles.FirstOrDefault(d => d.Producto_Id == productoSeleccionado.Id);
            int cantidadActualEnCarrito = detalleExistente?.Cantidad ?? 0;

            if (cantidadActualEnCarrito >= productoSeleccionado.Stock_Actual)
            {
                MessageBox.Show(
                    $"Stock insuficiente para '{productoSeleccionado.Nombre}'.\n" +
                    $"Stock disponible: {productoSeleccionado.Stock_Actual} unidad(es).",
                    "Sin stock",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Si ya está en el detalle, incrementar cantidad
            if (detalleExistente != null)
            {
                detalleExistente.Cantidad++;
            }
            else
            {
                Detalles.Add(new DetalleVenta
                {
                    Producto_Id = productoSeleccionado.Id,
                    Cantidad = 1,
                    Precio_Unitario = productoSeleccionado.Precio_Venta
                });
            }

            Mostrar();
        }

        private void DataVenta_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Verificar que es la columna Eliminar y una fila válida
                if (e.RowIndex < 0) return;
                if (dataVenta.Columns["btnEliminar"] == null) return;
                if (e.ColumnIndex != dataVenta.Columns["btnEliminar"].Index) return;

                // Obtener el Id del producto en esa fila y eliminar del detalle
                var idProducto = (int)dataVenta.Rows[e.RowIndex].Cells["Id"].Value;
                Detalles.RemoveAll(d => d.Producto_Id == idProducto);

                Mostrar();

            }
            catch (Exception )
            {

            }
        }

        public void Mostrar()
        {
            dataVenta.DataSource = null;
            dataVenta.DataSource = Detalles
                .Select(d =>
                {
                    var producto = Productos.FirstOrDefault(p => p.Id == d.Producto_Id);
                    return new
                    {
                        Id = d.Producto_Id,
                        Nombre = producto?.Nombre ?? "N/A",
                        Cantidad = d.Cantidad,
                        Precio = d.Precio_Unitario,
                        Subtotal = d.Cantidad * d.Precio_Unitario
                    };
                })
                .ToList();

            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            var total = Detalles.Sum(d => d.Cantidad * d.Precio_Unitario);
            txtTotal.Text = $"{total}";  
            Total = total;
        }

        private void AgregarColumnaEliminar()
        {
            if (dataVenta.Columns.Contains("btnEliminar")) return;

            var btnEliminar = new DataGridViewButtonColumn
            {
                Name = "btnEliminar",
                HeaderText = "Eliminar",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true
            };
            btnEliminar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnEliminar.DefaultCellStyle.ForeColor = Color.White;

            dataVenta.Columns.Add(btnEliminar);
        }

        private bool IndiceValido() =>
            Productos.Count > 0 &&
            IndexProducto >= 0 &&
            IndexProducto < Productos.Count;

        private void button4_Click(object sender, EventArgs e)
        {
            var corteActivo = _corteService.HayCorteAbierto();
            if (!corteActivo)
            {
                MessageBox.Show("Para realizar una compra necesita un corte activo.");
                return;
            }


            if (Detalles.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(Cliente == null)
            {
                Cliente.Id = 1;
            }

            var venta = new Venta
            {
                Fecha = DateTime.Now,
                Total = Total,
                Usuario_Id = Usuario.Id, // simulacion
                Corte_id = _corteService.ObtenerActivo().Corte_Id,
                Cliente_Id = Cliente.Id,
                Subtotal = Total,
                Descuento = 0,
                Metodo_Pago = cmbMetodo.Text
            };

            int resultado = _ventaService.Insertar(venta, Detalles);

            if (resultado > 0)
            {
                MessageBox.Show("Venta registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Limpiar formulario
                Detalles.Clear();
                Productos.Clear();
                this.Mostrar();
            }
            else
            {
                MessageBox.Show("Error al registrar la Venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataVenta.DataSource = null;
            Total = 0;
            txtTotal.Text = Total.ToString();
            txtProducto.Clear();
        }

        private void FmrVentasRealizar_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cliente cliente = null;
            if (txtCodigo.Text.Length > 0)
            {
                int Id = Convert.ToInt16(txtCodigo.Text);
                cliente = _clienteService.ObtenerPorId(Id);
            }

            if (cliente != null)
            {
                Cliente = cliente;
                txtCliente.Text = cliente.Nombre;
            }
            else
            {
                MessageBox.Show("Cliente no encontrado.");
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionesUI.ValidacionesUI.SoloNumerosConDecimal(e, (TextBox)sender);
        }
    }
}