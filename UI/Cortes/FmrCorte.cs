using BusinessLogic;
using Gimnasio.Entities;
using Gimnasio.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Cortes
{
    public partial class FmrCorte : Form
    {
        private readonly CorteService _corteService;
        private readonly VentaService _ventaService;
        private readonly CompraService _compraService;
        private CorteActivoVM _corteActual;
        public FmrCorte()
        {
            InitializeComponent();
            _corteService = new CorteService();

            if(!_corteService.HayCorteAbierto())
            {
                FmrCorteAbrir corteAbrir = new FmrCorteAbrir();
                corteAbrir.Show();
                this.Close();
                return;
            }
            _corteActual = _corteService.ObtenerActivo();
            _compraService = new CompraService();
            _ventaService = new VentaService();
            CargarTarjetas();
            CargarTxTResumen();
            cmbTipoV.SelectedIndex = 0;
        }

        private void CargarTarjetas()
        {
            lblTotalVentas.Text = $"L. {_corteActual.Ventas_Acumuladas:N2}";
            lblTotalCompras.Text = $"L. {_corteActual.Total_Compras:N2}";
            //lblEfectivoCaja.Text = $"L. {_corteActual.TotalEnCaja:N2}";
            lblTransacciones.Text = _corteActual.Total_Transacciones.ToString();
        }

        private void CargarTxTResumen()
        {
            txtEfectivoM.Text = $"L. {_corteActual.Efectivo:N2}";
            txtTarjetaM.Text = $"L. {_corteActual.Tarjeta:N2}";
            txtTransferenciaM.Text = $"L. {_corteActual.Transferencia:N2}";
            txtTotalM.Text = $"L. {_corteActual.Ventas_Acumuladas:N2}";

            txtMembresiaT.Text = $"L. {_corteActual.Por_Membresias:N2}";
            txtProductoT.Text = $"L. {_corteActual.Por_Productos:N2}";
            txtMixtasT.Text = $"L. {_corteActual.Por_Mixtas:N2}";
            txtTotalT.Text = $"L. {_corteActual.Ventas_Acumuladas:N2}";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("¿Está seguro de cerrar el corte de hoy?", "Confirmar corte", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                var corte_actual = _corteService.ObtenerPorId(_corteActual.Corte_Id);
                var corte = new Corte()
                {
                    Id = corte_actual.Id,
                    Usuario_Id = corte_actual.Usuario_Id,
                    Monto_Inicial = corte_actual.Monto_Inicial,
                    Fecha_Apertura = corte_actual.Fecha_Apertura,
                    Fecha_Cierre = DateTime.Now,
                    Total_Ventas = _corteActual.Ventas_Acumuladas,
                    Total_Compras = _corteActual.Total_Compras,
                    Total_Efectivo = _corteActual.Efectivo,
                    Total_Tarjeta = _corteActual.Tarjeta,
                    Total_Transferencia = _corteActual.Transferencia,
                    Total_Membresias = _corteActual.Por_Membresias,
                    Total_Productos = _corteActual.Por_Productos,
                    Gran_Total = _corteActual.Ventas_Acumuladas,
                    Estado = "cerrado",
                    Observaciones = txtObservaciones?.Text ?? string.Empty
                };
                _corteService.Cerrar(corte);
            }
        }
        

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tabControl1.SelectedIndex;

            if(index < 0)
            {
                return;
            }
            if(index == 0)
            {
                CargarTxTResumen();
                CargarTarjetas();
                LimpiarCompras();
                LimpiarVentas();
            }
            if(index == 1)
            {
                CargarTarjetas();
                CargarVentas();
                LimpiarCompras();
            }
            if(index == 2)

            {
                CargarTarjetas();
                CargarCompras();
                LimpiarVentas();
            }
        }

        // Ventas
        #region 
        private void CargarVentas()
        {
            CargarDataVentas();
            lblRegistroV.Text = $"Registros: {_ventaService.ObtenerPorCorte(_corteActual.Corte_Id).Count.ToString()}";
            txtTotalV.Text = $"L. {_corteActual.Ventas_Acumuladas:N2}";
        }

        private void LimpiarVentas()
        {
            dataVentas.DataSource = null;
        }

        private void CargarDataVentas()
        {
            if (txtBuscarV.Text.Length > 0)
            {

                var ventasFiltro = _ventaService.ObtenerPorCorte(_corteActual.Corte_Id)
                    .Where(v => v.Concepto.ToLower().Contains(txtBuscarV.Text.ToLower()))
                    .Select(v => new
                    {
                        v.Usuario,
                        v.Cliente,
                        v.Tipo_Venta,
                        v.Concepto,
                        v.Subtotal,
                        v.Fecha,

                    })
                    .ToList();
                dataVentas.DataSource = ventasFiltro;
                lblRegistroV.Text = $"Registros: {ventasFiltro.Count}";

            }
            else
            {
                var ventasFiltro = _ventaService.ObtenerPorCorte(_corteActual.Corte_Id)
                    .Select(v => new
                    {
                        v.Usuario,
                        v.Cliente,
                        v.Tipo_Venta,
                        v.Concepto,
                        v.Subtotal,
                        v.Fecha,

                    })
                    .ToList();
                dataVentas.DataSource = ventasFiltro;
                lblRegistroV.Text = $"Registros: {ventasFiltro.Count}";

            }
        }
        private void FiltrarVentas()
        {
            var indice = cmbTipoV.SelectedIndex;
            var categoria = cmbTipoV.Text;
            var texto = txtBuscarV.Text;
            // 0 - Todos
            // 1 - Membresías
            // 2 - Producto
            // 3 - Mixtas
            if (indice == 0)
            {
                CargarDataVentas();
            }
            if (indice > 0)
            {
                var ventasFiltro = _ventaService.ObtenerPorCorte(_corteActual.Corte_Id)
                    .Where(v => v.Concepto.ToLower().Contains(texto.ToLower())
                              && v.Tipo_Venta.ToLower().Equals(categoria.ToLower()))
                    .Select(v => new
                    {
                        v.Usuario,
                        v.Cliente,
                        v.Tipo_Venta,
                        v.Concepto,
                        v.Subtotal,
                        v.Fecha,

                    })
                    .ToList();
                dataVentas.DataSource = ventasFiltro;
                lblRegistroV.Text = $"Registros: {ventasFiltro.Count}";

            }
        }

        private void cmbTipoV_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarVentas();

        }

        private void btnRefrescarV_Click(object sender, EventArgs e)
        {
            CargarDataVentas();
            txtBuscarV.Clear();
            cmbTipoV.SelectedIndex = 0;
        }

        private void txtBuscarV_TextChanged(object sender, EventArgs e)
        {
            FiltrarVentas();
        }
        #endregion

        // Compras
        #region
        private void CargarCompras()
        {
            CargarDataCompras();
            CargarCmb();
            cmbTipoC.SelectedIndex = 0;
            lblRegistrosB.Text = $"Registros: {_compraService.ObtenerPorCorte(_corteActual.Corte_Id).Count.ToString()}";
            txtTotalC.Text = $"L. {_corteActual.Total_Compras:N2}";
        }

        private void CargarCmb()
        {
            var products = new ProductoService();
            cmbTipoC.DataSource = null;
            var categorias = products.ObtenerTodos()
                .Select(p => p.Categoria)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .ToList();

            categorias.Insert(0, "Todos");
            cmbTipoC.DataSource = categorias;

        }
        private void CargarDataCompras()
        {
            if (txtBuscarC.Text.Length > 0)
            {

                var comprasFiltro = _compraService.ObtenerPorCorte(_corteActual.Corte_Id)
                    .Where(v => v.Producto.ToLower().Contains(txtBuscarC.Text.ToLower()))
                    .Select(v => new
                    {
                        v.Usuario,
                        v.Proveedor,
                        v.Producto,
                        v.Categoria,
                        v.Subtotal,
                        v.Fecha,

                    })
                    .ToList();
                dataCompra.DataSource = comprasFiltro;
                lblRegistrosB.Text = $"Registros: {comprasFiltro.Count}";

            }
            else
            {
                var comprasFiltro = _compraService.ObtenerPorCorte(_corteActual.Corte_Id)
                    .Select(v => new
                    {
                        v.Usuario,
                        v.Proveedor,
                        v.Producto,
                        v.Categoria,
                        v.Subtotal,
                        v.Fecha,

                    })
                    .ToList();
                dataCompra.DataSource = comprasFiltro;
                lblRegistrosB.Text = $"Registros: {comprasFiltro.Count}";

            }
        }

        private void FiltrarCompras()
        {
            var indice = cmbTipoC.SelectedIndex;
            var categoria = cmbTipoC.Text;
            var texto = txtBuscarC.Text;
            // 0 - Todos
            // 1 - Membresías
            // 2 - Producto
            // 3 - Mixtas
            if (indice == 0)
            {
                CargarDataCompras();
            }
            if (indice > 0)
            {
                var comprasFiltro = _compraService.ObtenerPorCorte(_corteActual.Corte_Id)
                    .Where(v => v.Producto.ToLower().Contains(texto.ToLower())
                              && v.Categoria.ToLower().Equals(categoria.ToLower()))
                    .Select(v => new
                    {
                        v.Usuario,
                        v.Proveedor,
                        v.Producto,
                        v.Categoria,
                        v.Subtotal,
                        v.Fecha,

                    })
                    .ToList();
                dataCompra.DataSource = comprasFiltro;
                lblRegistrosB.Text = $"Registros: {comprasFiltro.Count}";

            }
        }

        private void LimpiarCompras()
        {
            dataCompra.DataSource = null;
        }
        #endregion

        private void txtBuscarC_TextChanged(object sender, EventArgs e)
        {
            FiltrarCompras();
        }

        private void cmbTipoC_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarCompras();
        }
    }
}
