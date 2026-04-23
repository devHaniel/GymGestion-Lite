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
using UI.Clientes;

namespace UI.Compras
{
    public partial class FmrCompras : Form
    {
        private readonly CompraService _compraService;
        private Usuario Usuario {  get; set; }
        public FmrCompras(Usuario usuario)
        {
            InitializeComponent();
            _compraService = new CompraService();
            this.Usuario = usuario;
            MostrarCompras(_compraService.ObtenerTodas());
            comboBox1.SelectedIndex = 0;
            Filtrar();
        }

        public void MostrarCompras(List<CompraVM> compras)
        {
            var resultado = compras.Select(c => new
            {
                Id = c.Id,
                Fecha = c.Fecha,
                Proveedor = c.Proveedor,
                Usuario = c.Usuario,
                Total = c.Total,
                Estado = c.Estado
            })
            .ToList();
            dataCompras.DataSource = resultado;
            AgregarColumnaDetalles();
            AgregarColumnaRecibida();
            AgregarColumnaCancelar();
            lblRegistros.Text = $"Registros: {compras.Count}";
        }

        public void AgregarColumnaDetalles()
        {
            if (dataCompras.Columns.Contains("btnDetalles")) return;

            DataGridViewButtonColumn btnDetalles = new DataGridViewButtonColumn();
            btnDetalles.Name = "btnDetalles";
            btnDetalles.HeaderText = "Detalles";
            btnDetalles.Text = "Detalles";
            btnDetalles.UseColumnTextForButtonValue = true;
            btnDetalles.DefaultCellStyle.BackColor = Color.IndianRed;
            btnDetalles.DefaultCellStyle.ForeColor = Color.White;

            dataCompras.Columns.Add(btnDetalles);
        }

        private void AgregarColumnaCancelar()
        {
            if (dataCompras.Columns.Contains("btnCancelar")) return;

            DataGridViewButtonColumn btnActualizar = new DataGridViewButtonColumn();
            btnActualizar.Name = "btnCancelar";
            btnActualizar.HeaderText = "Cancelar";
            btnActualizar.Text = "Cancelar";
            btnActualizar.UseColumnTextForButtonValue = true;
            btnActualizar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnActualizar.DefaultCellStyle.ForeColor = Color.White;

            dataCompras.Columns.Add(btnActualizar);
        }
        private void AgregarColumnaRecibida()
        {
            if (dataCompras.Columns.Contains("btnRecibido")) return;

            DataGridViewButtonColumn btnActualizar = new DataGridViewButtonColumn();
            btnActualizar.Name = "btnRecibido";
            btnActualizar.HeaderText = "Recibido";
            btnActualizar.Text = "Recibido";
            btnActualizar.UseColumnTextForButtonValue = true;
            btnActualizar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnActualizar.DefaultCellStyle.ForeColor = Color.White;

            dataCompras.Columns.Add(btnActualizar);
        }

        private void dataCompras_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                int index = e.RowIndex;
                var id = Convert.ToInt32(dataCompras.Rows[index].Cells["Id"].Value);

                if (dataCompras.Columns[e.ColumnIndex].Name == "btnRecibido")
                {
                    RecibirCompra(id);
                }
                else if (dataCompras.Columns[e.ColumnIndex].Name == "btnCancelar")
                {
                    CancelarCompra(id);
                }
                else if (dataCompras.Columns[e.ColumnIndex].Name == "btnDetalles")
                {
                    FmrCompraDetalles fmrCompraDetalles = new FmrCompraDetalles() { IdCompra = id};
                    fmrCompraDetalles.CargarDatos();
                    fmrCompraDetalles.ShowDialog();
                    //FmrClientesDetalles fmrClientesDetalles = new FmrClientesDetalles(this) { Detalles = true, IdCliente = id };
                    //fmrClientesDetalles.Modo();
                    //fmrClientesDetalles.ShowDialog();
                }
            }
            catch (Exception)
            {

            }
        }

        private void RecibirCompra(int id)
        {
            var confirmResult = MessageBox.Show("¿Está seguro de confirmar esta compra?", "Confirmar compra", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _compraService.ActualizarEstado(id, "recibida");
                MostrarCompras(_compraService.ObtenerTodas());
            }
        }

        private void CancelarCompra(int id)
        {
            var confirmResult = MessageBox.Show("¿Está seguro de cancelar esta compra?", "Confirmar cancelación", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _compraService.ActualizarEstado(id, "cancelada");
                MostrarCompras(_compraService.ObtenerTodas());
            }
        }

        private void dataCompras_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataCompras.Columns[e.ColumnIndex].Name == "Estado")
            {
                var valor = e.Value?.ToString();

                if (valor == "recibida")
                    e.CellStyle.BackColor = Color.LightGreen;

                else if (valor == "cancelada")
                    e.CellStyle.BackColor = Color.LightCoral;

                else if (valor == "pendiente")
                    e.CellStyle.BackColor = Color.Khaki;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            Filtrar();
        }

        private void txtFecha_ValueChanged(object sender, EventArgs e)
        {
            Filtrar();
        }

        private void Filtrar()
        {
            var compras = _compraService.ObtenerTodas();

            // Filtrar por estado
                string estado = comboBox1.Text.ToLower().Trim();
                compras = compras
                    .Where(c => c.Estado.ToLower().Trim() == estado)
                    .ToList();

            // Filtrar por fecha
            DateTime fecha = txtFecha.Value.Date;

            compras = compras
                .Where(c => c.Fecha.Date == fecha)
                .ToList();

            MostrarCompras(compras);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime fecha = txtFecha.Value.Date;

            MostrarCompras(_compraService.ObtenerTodas().Where(c => c.Fecha.Date == fecha)
                .ToList());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FmrComprasRealizar fmrComprasRealizar = new FmrComprasRealizar(Usuario);
            fmrComprasRealizar.ShowDialog();
        }
    }
}
