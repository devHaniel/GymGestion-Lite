using BusinessLogic;
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
using UI.Usuarios;

namespace UI.Clientes
{
    public partial class FmrClientes : Form
    {
        private readonly ClienteService _clienteService;

        public FmrClientes()
        {
            InitializeComponent();
            _clienteService = new ClienteService();
            cmbFiltro.SelectedIndex = 0;
            MostrarClientes(_clienteService.ObtenerTodos());
        }
        public void MostrarClientes(List<Cliente> clientes)
        {
            var resultado = clientes.Select(c => new
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Correo = c.Email,
                Documento = c.Documento, 
                Fecha_Nacimiento = c.Fecha_Nacimiento,
                Activo = c.Activo
            })
                .Where(c => c.Id != 1)
            .ToList();
            dataClientes.DataSource = resultado;
            AgregarColumnaDetalles();
            AgregarColumnaActualizar();
            AgregarColumnaEliminar();
            AgregarColumnaActivar();
            lblRegistros.Text = $"Registros: {clientes.Count}";
        }

        public void AgregarColumnaEliminar()
        {
            if (dataClientes.Columns.Contains("btnEliminar")) return;

            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "btnEliminar";
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            btnEliminar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnEliminar.DefaultCellStyle.ForeColor = Color.White;

            dataClientes.Columns.Add(btnEliminar);
        }

        public void AgregarColumnaDetalles()
        {
            if (dataClientes.Columns.Contains("btnDetalles")) return;

            DataGridViewButtonColumn btnDetalles = new DataGridViewButtonColumn();
            btnDetalles.Name = "btnDetalles";
            btnDetalles.HeaderText = "Detalles";
            btnDetalles.Text = "Detalles";
            btnDetalles.UseColumnTextForButtonValue = true;
            btnDetalles.DefaultCellStyle.BackColor = Color.IndianRed;
            btnDetalles.DefaultCellStyle.ForeColor = Color.White;

            dataClientes.Columns.Add(btnDetalles);
        }

        private void AgregarColumnaActualizar()
        {
            if (dataClientes.Columns.Contains("btnActualizar")) return;

            DataGridViewButtonColumn btnActualizar = new DataGridViewButtonColumn();
            btnActualizar.Name = "btnActualizar";
            btnActualizar.HeaderText = "Actualizar";
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseColumnTextForButtonValue = true;
            btnActualizar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnActualizar.DefaultCellStyle.ForeColor = Color.White;

            dataClientes.Columns.Add(btnActualizar);
        }

        public void AgregarColumnaActivar()
        {
            if (dataClientes.Columns.Contains("btnActivar")) return;

            DataGridViewButtonColumn btnActivar = new DataGridViewButtonColumn();
            btnActivar.Name = "btnActivar";
            btnActivar.HeaderText = "Activar";
            btnActivar.Text = "Activar";
            btnActivar.UseColumnTextForButtonValue = true;
            btnActivar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnActivar.DefaultCellStyle.ForeColor = Color.White;

            dataClientes.Columns.Add(btnActivar);
        }

        private void dataClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                int index = e.RowIndex;
                var id = Convert.ToInt32(dataClientes.Rows[index].Cells["Id"].Value);

                if (dataClientes.Columns[e.ColumnIndex].Name == "btnActualizar")
                {
                    FmrClientesDetalles fmrClientesDetalles = new FmrClientesDetalles(this) { Editar = true, IdCliente = id };
                    fmrClientesDetalles.Modo();
                    fmrClientesDetalles.ShowDialog();
                }
                else if (dataClientes.Columns[e.ColumnIndex].Name == "btnEliminar")
                {
                    EliminarCliente(id);
                }
                else if (dataClientes.Columns[e.ColumnIndex].Name == "btnActivar")
                {
                    ActivarCliente(id);
                }
                else if (dataClientes.Columns[e.ColumnIndex].Name == "btnDetalles")
                {
                    FmrClientesDetalles fmrClientesDetalles = new FmrClientesDetalles(this) { Detalles = true, IdCliente = id };
                    fmrClientesDetalles.Modo();
                    fmrClientesDetalles.ShowDialog();
                }
            }
            catch(Exception)
            {

            }
        }

        private void EliminarCliente(int id)
        {
            var confirmResult = MessageBox.Show("¿Está seguro de eliminar este cliente?", "Confirmar eliminación", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _clienteService.Eliminar(id);
                MostrarClientes(_clienteService.ObtenerTodos());
            }
        }
        private void ActivarCliente(int id)
        {
            var confirmResult = MessageBox.Show("¿Está seguro de activar este cliente?", "Confirmar activación", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _clienteService.Activar(id);
                MostrarClientes(_clienteService.ObtenerTodos());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FmrClientesDetalles fmrClientesDetalles = new FmrClientesDetalles(this) { Nuevo = true };
            fmrClientesDetalles.Modo();
            fmrClientesDetalles.ShowDialog();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // 0 - inactivo
            // 1 - activo
            int result = 0;
            if (cmbFiltro.SelectedIndex == 0)
                result = 1;

            MostrarClientes(_clienteService.Buscar(txtBuscar.Text, result));
        }

        private void cmbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (cmbFiltro.SelectedIndex == 0)
                result = 1;

            MostrarClientes(_clienteService.Buscar(txtBuscar.Text, result));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MostrarClientes(_clienteService.ObtenerTodos());
        }
    }
}
