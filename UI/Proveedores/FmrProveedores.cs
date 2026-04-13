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

namespace UI.Proveedores
{
    public partial class FmrProveedores : Form
    {
        private readonly ProveedorService _proveedorService;
        public FmrProveedores()
        {
            InitializeComponent();
            _proveedorService = new ProveedorService();
            MostrarProveedores(_proveedorService.ObtenerTodos());
        }


        public void MostrarProveedores(List<Proveedor> proveedores)
        {
            dataProductos.DataSource = proveedores;
            AgregarColumnaDetalles();
            AgregarColumnaActualizar();
            AgregarColumnaEliminar();
            lblRegistros.Text = $"Registros: {proveedores.Count}";
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var proveedores = _proveedorService.ObtenerTodos().Where(p => p.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
            MostrarProveedores(proveedores);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MostrarProveedores(_proveedorService.ObtenerTodos());
            txtBuscar.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FmrProveedoresDetalle fmrProveedoresDetalle = new FmrProveedoresDetalle(this) { Nuevo = true };
            fmrProveedoresDetalle.Modo();
            fmrProveedoresDetalle.ShowDialog();
        }

        private void dataProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int index = e.RowIndex;
            var id = Convert.ToInt32(dataProductos.Rows[index].Cells["Id"].Value);

            if (dataProductos.Columns[e.ColumnIndex].Name == "btnActualizar")
            {
                FmrProveedoresDetalle fmrProveedoresDetalle = new FmrProveedoresDetalle(this) { Editar = true, IdProveedor = id };
                fmrProveedoresDetalle.Modo();
                fmrProveedoresDetalle.ShowDialog();
            }
            else if (dataProductos.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                EliminarProveedor(id);
            }
            else if (dataProductos.Columns[e.ColumnIndex].Name == "btnDetalles")
            {
                FmrProveedoresDetalle fmrProveedoresDetalle = new FmrProveedoresDetalle(this) { Detalles = true, IdProveedor = id };
                fmrProveedoresDetalle.Modo();
                fmrProveedoresDetalle.ShowDialog();
            }
        }

        private void EliminarProveedor(int id)
        {
            var confirmResult = MessageBox.Show("¿Está seguro de eliminar este proveedor?", "Confirmar eliminación", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _proveedorService.Eliminar(id);
                MostrarProveedores(_proveedorService.ObtenerTodos());
            }
        }
    }
}
