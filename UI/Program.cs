using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Cortes;
using UI.Login;
using UI.Proveedores;

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
            //var login = new FmrLogin();

            //if (login.ShowDialog() == DialogResult.OK)
            //{
            //    Application.Run(new FmrMain() { Usuario = login.Usuario });
            //}
            Application.Run(new FmrProveedores());
        }
    }
}
