using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class Calculadora
    {
        /// <summary>
        /// Devuelve el resultado de la operacion
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <param name="operador"></param>
        /// <returns></returns> El resultado de la operacion
        public static double Operar(Numero num1, Numero num2, string operador)
        {
            double resultadoOperacion = 0;

            if (ValidarOperador(operador) == "+")
            {
                resultadoOperacion = num1 + num2;
            }
            else if (ValidarOperador(operador) == "-")
            {
                resultadoOperacion = num1 - num2;
            }
            else if (ValidarOperador(operador) == "*")
            {
                resultadoOperacion = num1 * num2;
            }
            else if (ValidarOperador(operador) == "/")
            {
                if (num2 == null)
                {
                    resultadoOperacion = double.MinValue;
                }
                else
                {
                    resultadoOperacion = num1 / num2;
                }
            }

            return resultadoOperacion;
        }

        /// <summary>
        /// Valida que el operador sea suma, resta, multiplicacion o division
        /// </summary>
        /// <param name="operador"></param>
        /// <returns></returns> El operador
        private static string ValidarOperador(string operador)
        {
            string value = "+";

            if (operador == "+")
            {
                value = "+";
            }
            else if (operador == "-")
            {
                value = "-";
            }
            else if (operador == "*")
            {
                value = "*";
            }
            else if (operador == "/")
            {
                value = "/";
            }
            else
            {
                value = "+";
            }

            return value;
        }
    }
}
