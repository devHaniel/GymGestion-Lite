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

namespace UI
{
    public partial class FmrDatabaseOperaciones : Form
    {
        private readonly DatabaseOperaciones _databaseOperaciones;
        public FmrDatabaseOperaciones()
        {
            InitializeComponent();
            _databaseOperaciones = new DatabaseOperaciones();
        }

        private void btnOperaciones_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivos de respaldo (*.bak)|*.bak";
                sfd.FileName = $"Respaldo_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                sfd.Title = "Guardar respaldo";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    bool exito = _databaseOperaciones.BackupDatabase(sfd.FileName);

                    if (exito)
                        MessageBox.Show("✅ Respaldo creado exitosamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("❌ Error al crear el respaldo.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
            "⚠️ Esto SOBRESCRIBIRÁ la base de datos actual.\n¿Deseas continuar?",
            "Confirmar restauración",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos de respaldo (*.bak)|*.bak";
                ofd.Title = "Seleccionar archivo de respaldo";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    bool exito = _databaseOperaciones.RestoreDatabase( ofd.FileName);

                    if (exito)
                        MessageBox.Show("✅ Base de datos restaurada.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("❌ Error al restaurar.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
