using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Cortes;

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
            Application.Run(new FmrCorteMain());

            //var corteService = new CorteService();

            //if (!corteService.HayCorteAbierto())
            //{
            //    Application.Run(new FmrCorteAbrir());

            //}

            //if (corteService.HayCorteAbierto())
            //{
            //    Application.Run(new FmrCorte());

            //}
        }
    }
}
