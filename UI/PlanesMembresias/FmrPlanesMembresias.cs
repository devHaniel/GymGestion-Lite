using BusinessLogic;
using Entities;
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
using UI.Categorias;

namespace UI.PlanesMembresias
{
    public partial class FmrPlanesMembresias : Form
    {
        private readonly PlanMembresiaService _planMembresiaService;
        public FmrPlanesMembresias()
        {
            InitializeComponent();
            _planMembresiaService = new PlanMembresiaService();
            MostrarPlanes(_planMembresiaService.ObtenerTodos());
        }

        public void MostrarPlanes(List<PlanMembresia> planes)
        {
            dataPlanes.DataSource = planes;
            AgregarColumnaDetalles();
            AgregarColumnaActualizar();
            AgregarColumnaEliminar();
            lblRegistros.Text = $"Registros: {planes.Count}";
        }

        public void AgregarColumnaEliminar()
        {
            if (dataPlanes.Columns.Contains("btnEliminar")) return;

            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "btnEliminar";
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            btnEliminar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnEliminar.DefaultCellStyle.ForeColor = Color.White;

            dataPlanes.Columns.Add(btnEliminar);
        }

        public void AgregarColumnaDetalles()
        {
            if (dataPlanes.Columns.Contains("btnDetalles")) return;

            DataGridViewButtonColumn btnDetalles = new DataGridViewButtonColumn();
            btnDetalles.Name = "btnDetalles";
            btnDetalles.HeaderText = "Detalles";
            btnDetalles.Text = "Detalles";
            btnDetalles.UseColumnTextForButtonValue = true;
            btnDetalles.DefaultCellStyle.BackColor = Color.IndianRed;
            btnDetalles.DefaultCellStyle.ForeColor = Color.White;

            dataPlanes.Columns.Add(btnDetalles);
        }

        private void AgregarColumnaActualizar()
        {
            if (dataPlanes.Columns.Contains("btnActualizar")) return;

            DataGridViewButtonColumn btnActualizar = new DataGridViewButtonColumn();
            btnActualizar.Name = "btnActualizar";
            btnActualizar.HeaderText = "Actualizar";
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseColumnTextForButtonValue = true;
            btnActualizar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnActualizar.DefaultCellStyle.ForeColor = Color.White;

            dataPlanes.Columns.Add(btnActualizar);
        }

        private void dataPlanes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int index = e.RowIndex;
            var id = Convert.ToInt32(dataPlanes.Rows[index].Cells["Id"].Value);

            if (dataPlanes.Columns[e.ColumnIndex].Name == "btnActualizar")
            {
                FmrPlanesMembresiasDetalles fmrPlanesMembresiasDetalles = new FmrPlanesMembresiasDetalles(this) { Editar = true, IdPlan = id };
                fmrPlanesMembresiasDetalles.Modo();
                fmrPlanesMembresiasDetalles.ShowDialog();
            }
            else if (dataPlanes.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                EliminarPlan(id);
            }
            else if (dataPlanes.Columns[e.ColumnIndex].Name == "btnDetalles")
            {
                FmrPlanesMembresiasDetalles fmrPlanesMembresiasDetalles = new FmrPlanesMembresiasDetalles(this) { Detalles = true, IdPlan = id };
                fmrPlanesMembresiasDetalles.Modo();
                fmrPlanesMembresiasDetalles.ShowDialog();
            }
        }

        private void EliminarPlan(int id)
        {
            var confirmResult = MessageBox.Show("¿Está seguro de eliminar este plan?", "Confirmar eliminación", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _planMembresiaService.Eliminar(id);
                MostrarPlanes(_planMembresiaService.ObtenerTodos());
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            var planes = _planMembresiaService.ObtenerTodos().Where(p => p.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
            MostrarPlanes(planes);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FmrPlanesMembresiasDetalles fmrPlanesMembresiasDetalles = new FmrPlanesMembresiasDetalles(this) { Nuevo = true };
            fmrPlanesMembresiasDetalles.Modo();
            fmrPlanesMembresiasDetalles.ShowDialog();
        }
    }
}
