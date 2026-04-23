using BusinessLogic;
using Entities;
using Entities.VistaModelos;
using Gimnasio.BusinessLogic;
using Gimnasio.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Categorias;

namespace UI.PlanesMembresias
{
    public partial class FmrPlanesMembresiasDetalles : Form
    {
        private readonly FmrPlanesMembresias   _fmrPlanesMembresias;
        private readonly PlanMembresiaService _planMembresiaService;
        public bool Nuevo { get; set; } = false;
        public bool Editar { get; set; } = false;
        public int IdPlan { get; set; }
        public bool Detalles { get; set; } = false;
        public FmrPlanesMembresiasDetalles(FmrPlanesMembresias fmrPlanesMembresias)
        {
            InitializeComponent();
            _fmrPlanesMembresias = fmrPlanesMembresias;
            _planMembresiaService = new PlanMembresiaService();
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
                txtCodigo.Text = IdPlan.ToString();
                CargarDatos();
            }
            else if (Detalles)
            {
                lblEstado.Text = "Modo: Detalles del Registro";
                txtNombre.ReadOnly = true;
                txtDescripcion.ReadOnly = true;
                txtPrecio.ReadOnly = true;
                txtDuracion.ReadOnly = true;
                btnGuardar.Visible = false;
                btnLimpiar.Visible = false;
                CargarDatos();

            }
        }

        public void CargarDatos()
        {
            if (IdPlan > 0)
            {
                var plan = _planMembresiaService.ObtenerPorId(IdPlan);
                txtCodigo.Text = plan.Id.ToString();
                txtNombre.Text = plan.Nombre;
                txtDescripcion.Text = plan.Descripcion;
                txtPrecio.Text = plan.Precio.ToString();
                txtDuracion.Value = plan.Duracion_Dias;
                if (plan.Activo)
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
                GuardarPlan();
            }
            if (Editar)
            {
                ActualizarPlan();
            }
        }

        private void ActualizarPlan()
        {
            var nombre = txtNombre.Text;
            var descripcion = txtDescripcion.Text;
            var precio = Convert.ToDecimal(txtPrecio.Text);
            var duracion = Convert.ToInt16(txtDuracion.Value);
            var activo = cmbActivo.Checked;
            var plan = new PlanMembresia()
            {
                Id = IdPlan,
                Nombre = nombre,
                Descripcion = descripcion,
                Precio = precio,
                Duracion_Dias = duracion,
                Activo = activo
            };


            var result = _planMembresiaService.Actualizar(plan);
            if (result)
            {
                MessageBox.Show("Plan actualizado correctamente.");
                _fmrPlanesMembresias.MostrarPlanes(_planMembresiaService.ObtenerTodos());
                this.Close();
            }
            else
            {
                MessageBox.Show("Error en actualizar el Plan");
            }
        }

        private void GuardarPlan()
        {
            var nombre = txtNombre.Text;
            var descripcion = txtDescripcion.Text;
            var precio = Convert.ToDecimal(txtPrecio.Text);
            var duracion = Convert.ToInt16(txtDuracion.Value);
            var activo = cmbActivo.Checked;
            var plan = new PlanMembresia()
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Precio = precio,
                Duracion_Dias = duracion,
                Activo = activo
            };

            var result = _planMembresiaService.Insertar(plan);
            if (result > 0)
            {
                MessageBox.Show("Plan ingresado correctamente.");
                _fmrPlanesMembresias.MostrarPlanes(_planMembresiaService.ObtenerTodos());
                this.Close();
            }
            else
            {
                MessageBox.Show("Error en ingresar el Plan");
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
