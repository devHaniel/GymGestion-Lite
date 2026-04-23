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

namespace UI.Cortes
{
    public partial class FmrCorteAbrir : Form
    {
        private readonly CorteService _corteService;
        public Usuario Usuario { get; set; } 
        public FmrCorteAbrir(Usuario usuario)
        {
            InitializeComponent();
            this.Usuario = usuario;
            _corteService = new CorteService();
            txtFechaApertura.Text = DateTime.Now.ToString("g");
            txtUsuario.Text = Usuario.Nombre;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text.Length > 0)
            {
                var monto = decimal.Parse(txtMonto.Text.Trim());
                var fecha = DateTime.Now;
                var corte = new Corte() 
                { 
                    //UsuarioId = Usuario.Id,
                    Usuario_Id = Usuario.Id,
                    Fecha_Apertura = fecha,
                    Monto_Inicial = monto
                };
                var result = _corteService.Abrir(corte);

                if(result > 0)
                {
                    MessageBox.Show("Corte abierto correctamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No fue posible abrir el corte.");
                }
            }
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidacionesUI.ValidacionesUI.SoloNumerosConDecimal(e, (TextBox)sender);
        }
    }
}
