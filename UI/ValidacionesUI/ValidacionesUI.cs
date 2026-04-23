using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.ValidacionesUI
{
    static class ValidacionesUI
    {
        public static void SoloNumerosConDecimal(KeyPressEventArgs e, TextBox txt)
        {
            char decimalSeparator = System.Globalization.CultureInfo
                .CurrentCulture.NumberFormat.NumberDecimalSeparator[0];

            // Permitir números, control (backspace) y separador decimal
            if (!char.IsDigit(e.KeyChar) &&
                !char.IsControl(e.KeyChar) &&
                e.KeyChar != decimalSeparator)
            {
                e.Handled = true;
            }

            // Evitar más de un separador decimal
            if (e.KeyChar == decimalSeparator && txt.Text.Contains(decimalSeparator))
            {
                e.Handled = true;
            }
        }
    }
}
