using BusinessLogic;
using Entities;
using Entities.VistaModelos;
using Gimnasio.BusinessLogic;
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

namespace UI.Categorias
{
    public partial class FmrCategorias : Form
    {
        private readonly CategoriaService _categoriaService;
        public FmrCategorias()
        {
            InitializeComponent();
            _categoriaService = new CategoriaService();
            MostrarCategorias(_categoriaService.ObtenerTodos());
        }

        public void MostrarCategorias(List<Categoria> categoria)
        {
            dataCategorias.DataSource = categoria ;
            AgregarColumnaDetalles();
            AgregarColumnaActualizar();
            AgregarColumnaEliminar();
            lblRegistros.Text = $"Registros: {categoria.Count}";
        }

        public void AgregarColumnaEliminar()
        {
            if (dataCategorias.Columns.Contains("btnEliminar")) return;

            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "btnEliminar";
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            btnEliminar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnEliminar.DefaultCellStyle.ForeColor = Color.White;

            dataCategorias.Columns.Add(btnEliminar);
        }

        public void AgregarColumnaDetalles()
        {
            if (dataCategorias.Columns.Contains("btnDetalles")) return;

            DataGridViewButtonColumn btnDetalles = new DataGridViewButtonColumn();
            btnDetalles.Name = "btnDetalles";
            btnDetalles.HeaderText = "Detalles";
            btnDetalles.Text = "Detalles";
            btnDetalles.UseColumnTextForButtonValue = true;
            btnDetalles.DefaultCellStyle.BackColor = Color.IndianRed;
            btnDetalles.DefaultCellStyle.ForeColor = Color.White;

            dataCategorias.Columns.Add(btnDetalles);
        }

        private void AgregarColumnaActualizar()
        {
            if (dataCategorias.Columns.Contains("btnActualizar")) return;

            DataGridViewButtonColumn btnActualizar = new DataGridViewButtonColumn();
            btnActualizar.Name = "btnActualizar";
            btnActualizar.HeaderText = "Actualizar";
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseColumnTextForButtonValue = true;
            btnActualizar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnActualizar.DefaultCellStyle.ForeColor = Color.White;

            dataCategorias.Columns.Add(btnActualizar);
        }

        private void dataCategorias_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int index = e.RowIndex;
            var id = Convert.ToInt32(dataCategorias.Rows[index].Cells["Id"].Value);

            if (dataCategorias.Columns[e.ColumnIndex].Name == "btnActualizar")
            {
                FmrCategoriasDetalles fmrCategoriasDetalles = new FmrCategoriasDetalles(this) { Editar = true, IdCategoria = id };
                fmrCategoriasDetalles.Modo();
                fmrCategoriasDetalles.ShowDialog();
            }
            else if (dataCategorias.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                EliminarCategoria(id);
            }
            else if (dataCategorias.Columns[e.ColumnIndex].Name == "btnDetalles")
            {
                FmrCategoriasDetalles fmrCategoriasDetalles = new FmrCategoriasDetalles(this) { Detalles = true, IdCategoria = id };
                fmrCategoriasDetalles.Modo();
                fmrCategoriasDetalles.ShowDialog();
            }
        }
        private void EliminarCategoria(int id)
        {
            var confirmResult = MessageBox.Show("¿Está seguro de eliminar esta categoria?", "Confirmar eliminación", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _categoriaService.Eliminar(id);
                MostrarCategorias(_categoriaService.ObtenerTodos());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FmrCategoriasDetalles fmrCategoriasDetalles = new FmrCategoriasDetalles(this) { Nuevo = true };
            fmrCategoriasDetalles.Modo();
            fmrCategoriasDetalles.ShowDialog();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            var categorias = _categoriaService.ObtenerTodos().Where(p => p.Nombre.ToLower().Contains(txtBuscar.Text.ToLower())).ToList();
            MostrarCategorias(categorias);
        }
    }
}
