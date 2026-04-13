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
using UI.Cortes;

namespace UI
{
    public partial class FmrMain : Form
    {
        public Usuario Usuario { get; set; }
        public FmrMain()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            FmrCorteMain fmrCorteMain = new FmrCorteMain() { Usuario = this.Usuario };
            fmrCorteMain.ShowDialog();
        }
    }
}
