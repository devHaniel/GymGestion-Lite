using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Clientes;
using UI.Compras;
using UI.Cortes;
using UI.Login;
using UI.PlanesMembresias;
using UI.Productos;
using UI.Proveedores;
using UI.Ventas;

namespace UI
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var login = new FmrLogin())
            {
                if (login.ShowDialog() == DialogResult.OK)
                    Application.Run(new FmrMain(login.Usuario));
            }
        }
    }
}
