using BusinessLogic;
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

namespace UI
{
    public partial class FmrUsuarios : Form
    {
        private readonly UsuarioService _usuarioService;
        public FmrUsuarios()
        {
            InitializeComponent();
            _usuarioService = new UsuarioService();
            comboBox1.SelectedIndex = 0;
            Activos_Inactivos();
        }


        public void MostrarUsuarios()
        {
            var usuarios = _usuarioService
                .ObtenerTodos()
                .Select(user => new {Id = user.Id, Nombre = user.Nombre, Apellido = user.Apellido, Email = user.Email, Telefono = user.Telefono, Activo = user.Activo})
                .ToList();

            dataUsuarios.DataSource = usuarios;
            AgregarColumnaDetalles();
            AgregarColumnaActualizar();
            AgregarColumnaEliminar();
            AgregarColumnaActivar();
            lblRegistros.Text = $"Registros: {usuarios.Count}";
        }

        public void AgregarColumnaEliminar()
        {
            if (dataUsuarios.Columns.Contains("btnEliminar")) return;

            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "btnEliminar";
            btnEliminar.HeaderText = "Inactivar";
            btnEliminar.Text = "Inactivar";
            btnEliminar.UseColumnTextForButtonValue = true;
            btnEliminar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnEliminar.DefaultCellStyle.ForeColor = Color.White;

            dataUsuarios.Columns.Add(btnEliminar);
        }

        public void AgregarColumnaActivar()
        {
            if (dataUsuarios.Columns.Contains("btnActivar")) return;

            DataGridViewButtonColumn btnActivar = new DataGridViewButtonColumn();
            btnActivar.Name = "btnActivar";
            btnActivar.HeaderText = "Activar";
            btnActivar.Text = "Activar";
            btnActivar.UseColumnTextForButtonValue = true;
            btnActivar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnActivar.DefaultCellStyle.ForeColor = Color.White;

            dataUsuarios.Columns.Add(btnActivar);
        }

        public void AgregarColumnaDetalles()
        {
            if (dataUsuarios.Columns.Contains("btnDetalles")) return;

            DataGridViewButtonColumn btnDetalles = new DataGridViewButtonColumn();
            btnDetalles.Name = "btnDetalles";
            btnDetalles.HeaderText = "Detalles";
            btnDetalles.Text = "Detalles";
            btnDetalles.UseColumnTextForButtonValue = true;
            btnDetalles.DefaultCellStyle.BackColor = Color.IndianRed;
            btnDetalles.DefaultCellStyle.ForeColor = Color.White;

            dataUsuarios.Columns.Add(btnDetalles);
        }

        private void AgregarColumnaActualizar()
        {
            if (dataUsuarios.Columns.Contains("btnActualizar")) return;

            DataGridViewButtonColumn btnActualizar = new DataGridViewButtonColumn();
            btnActualizar.Name = "btnActualizar";
            btnActualizar.HeaderText = "Actualizar";
            btnActualizar.Text = "Actualizar";
            btnActualizar.UseColumnTextForButtonValue = true;
            btnActualizar.DefaultCellStyle.BackColor = Color.IndianRed;
            btnActualizar.DefaultCellStyle.ForeColor = Color.White;

            dataUsuarios.Columns.Add(btnActualizar);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MostrarUsuarios();
            comboBox1.SelectedIndex = 0;
            textBox1.Text = "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Activos_Inactivos();

        }

        private void dataUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int index = e.RowIndex;
            var id = Convert.ToInt32(dataUsuarios.Rows[index].Cells["Id"].Value);

            if (dataUsuarios.Columns[e.ColumnIndex].Name == "btnActualizar")
            {
                FmrUsuariosDetalle fmrUsuariosDetalle = new FmrUsuariosDetalle(this) { Editar = true , IdUsuario = id};
                fmrUsuariosDetalle.Modo();
                fmrUsuariosDetalle.ShowDialog();
            }
            else if (dataUsuarios.Columns[e.ColumnIndex].Name == "btnEliminar")
            {
                EliminarUsuario(id);
            }
            else if (dataUsuarios.Columns[e.ColumnIndex].Name == "btnActivar")
            {
                ActivarUsuario(id);
            }
            else if (dataUsuarios.Columns[e.ColumnIndex].Name == "btnDetalles")
            {
                FmrUsuariosDetalle fmrUsuariosDetalle = new FmrUsuariosDetalle(this) { Detalles = true, IdUsuario = id };
                fmrUsuariosDetalle.Modo();
                fmrUsuariosDetalle.ShowDialog();
            }
        }

        private void EliminarUsuario(int id)
        {
            var confirmResult = MessageBox.Show("¿Está seguro de eliminar este usuario?", "Confirmar eliminación", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _usuarioService.Eliminar(id);
                Activos_Inactivos();
            }
        }
        private void ActivarUsuario(int id)
        {
            var confirmResult = MessageBox.Show("¿Está seguro de activar este usuario?", "Confirmar activación", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                _usuarioService.Activar(id);
                Activos_Inactivos();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Activos_Inactivos();
        }

        public void Activos_Inactivos()
        {
            bool activo = comboBox1.SelectedIndex == 1 ? true : false;
            bool todos = comboBox1.SelectedIndex == 0 ? true : false;

            if (todos)
            {
                var usuarios = _usuarioService
                .ObtenerTodos()
                .Select(user => new { Id = user.Id, Nombre = user.Nombre, Apellido = user.Apellido, Email = user.Email, Telefono = user.Telefono, Activo = user.Activo })
                .Where(u => u.Nombre.ToLower().Contains(textBox1.Text.ToLower()))
                .ToList();
                dataUsuarios.DataSource = usuarios;
                AgregarColumnaDetalles();
                AgregarColumnaActualizar();
                AgregarColumnaEliminar();
                AgregarColumnaActivar();
                lblRegistros.Text = $"Registros: {usuarios.Count}";
                
            }
            else
            {
                var usuarios = _usuarioService
                    .ObtenerTodos()
                    .Select(user => new { Id = user.Id, Nombre = user.Nombre, Apellido = user.Apellido, Email = user.Email, Telefono = user.Telefono, Activo = user.Activo })
                    .Where(u => u.Nombre.ToLower().Contains(textBox1.Text.ToLower()) && u.Activo == activo)
                    .ToList();

                dataUsuarios.DataSource = usuarios;
                AgregarColumnaDetalles();
                AgregarColumnaActualizar();
                AgregarColumnaEliminar();
                AgregarColumnaActivar() ;
                lblRegistros.Text = $"Registros: {usuarios.Count}";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MostrarUsuarios();
            comboBox1.SelectedIndex = 0;
            textBox1.Text = "";
        }


        private void button4_Click(object sender, EventArgs e)
        {
            FmrUsuariosDetalle fmrUsuariosDetalle = new FmrUsuariosDetalle(this) { Nuevo = true};
            fmrUsuariosDetalle.Modo();
            fmrUsuariosDetalle.ShowDialog();
        }
    }
}
