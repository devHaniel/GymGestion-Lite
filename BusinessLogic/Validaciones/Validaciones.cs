using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLogic.Validaciones
{
    public static class Validaciones
    {
        public static bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            return Regex.IsMatch(correo,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool EsNumeroPositivo(string valor)
        {
            if (int.TryParse(valor, out int numero))
            {
                return numero > 0;
            }
            return false;
        }

        public static bool EsNumeroPositivo(int valor)
        {
            return valor >= 0;
        }

        public static bool EsDecimalPositivo(string valor)
        {
            if (decimal.TryParse(valor, out decimal numero))
            {
                return numero > 0;
            }
            return false;
        }

        public static bool NoVacio(string texto)
        {
            return !string.IsNullOrWhiteSpace(texto);
        }

        public static bool SoloLetras(string texto)
        {
            return Regex.IsMatch(texto, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$");
        }

        public static bool SoloNumeros(string texto)
        {
            return Regex.IsMatch(texto, @"^\d+$");
        }

        public static bool LongitudMinima(string texto, int longitud)
        {
            return texto != null && texto.Length >= longitud;
        }

        public static bool EnRango(int valor, int min, int max)
        {
            return valor >= min && valor <= max;
        }
    }
}
