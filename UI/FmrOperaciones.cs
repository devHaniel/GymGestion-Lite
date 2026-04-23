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
using UI.Ventas;

namespace UI
{
    public partial class FmrOperaciones : Form
    {
        public Usuario Usuario { get; set; }
        public FmrOperaciones(Usuario usuario)
        {
            InitializeComponent();
            this.Usuario = usuario;
        }

        private void AbrirForm(Form formHijo)
        {
            panelVistas.Controls.Clear();

            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.None; // IMPORTANTE

            panelVistas.Controls.Add(formHijo);
            formHijo.Show();
            formHijo.Location = new Point(
            (panelVistas.Width - formHijo.Width) / 2,
            (panelVistas.Height - formHijo.Height) / 2
            );
        }

        private void btnRegistro_Click(object sender, EventArgs e)
        {
            FmrClientesRegistro fmrClientesRegistro = new FmrClientesRegistro(Usuario);
            AbrirForm(fmrClientesRegistro);

        }

        private void btnVenta_Click(object sender, EventArgs e)
        {
            FmrVentasRealizar fmrVentasRealizar = new FmrVentasRealizar(Usuario);
            AbrirForm(fmrVentasRealizar);
        }
    }
}
