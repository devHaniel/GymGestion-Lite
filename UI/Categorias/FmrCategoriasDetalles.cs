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
using UI.Proveedores;

namespace UI.Categorias
{
    public partial class FmrCategoriasDetalles : Form
    {
        private readonly CategoriaService _categoriaService;
        private readonly FmrCategorias _fmrCategorias;
        public bool Nuevo { get; set; } = false;
        public bool Editar { get; set; } = false;
        public int IdCategoria { get; set; } 
        public bool Detalles { get; set; } = false;
        public FmrCategoriasDetalles(FmrCategorias fmrCategorias)
        {
            InitializeComponent();
            _fmrCategorias = fmrCategorias;
            _categoriaService = new CategoriaService();
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
                txtCodigo.Text = IdCategoria.ToString();
                CargarDatos();
            }
            else if (Detalles)
            {
                lblEstado.Text = "Modo: Detalles del Registro";
                txtNombre.ReadOnly = true;
                btnGuardar.Visible = false;
                btnLimpiar.Visible = false;
                CargarDatos();

            }
        }

        public void CargarDatos()
        {
            if (IdCategoria > 0)
            {
                var categoria = _categoriaService.ObtenerPorId(IdCategoria);
                txtCodigo.Text = categoria.Id.ToString();
                txtNombre.Text = categoria.Nombre;
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Nuevo)
            {
                GuardarProveedor();
            }
            if (Editar)
            {
                ActualizarProveedor();
            }
        }

        private void ActualizarProveedor()
        {
            var nombre = txtNombre.Text;

            var categoria = new Categoria()
            {
                Id = IdCategoria,
                Nombre = nombre,
            };


            var result = _categoriaService.Actualizar(categoria);
            if (result)
            {
                MessageBox.Show("Categoria actualizado correctamente.");
                _fmrCategorias.MostrarCategorias(_categoriaService.ObtenerTodos());
                this.Close();
            }
            else
            {
                MessageBox.Show("Error en actualizar al categoria");
            }
        }

        private void GuardarProveedor()
        {
            var nombre = txtNombre.Text;

            var categoria = new Categoria()
            {
                Nombre = nombre,
            };

            var result = _categoriaService.Insertar(categoria);
            if (result > 0)
            {
                MessageBox.Show("Categoria ingresado correctamente.");
                _fmrCategorias.MostrarCategorias(_categoriaService.ObtenerTodos());
                this.Close();
            }
            else
            {
                MessageBox.Show("Error en ingresar al Categoria");
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}