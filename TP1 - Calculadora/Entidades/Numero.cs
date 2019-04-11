using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Numero
    {
        private double numero;

        public string SetNumero { set { this.numero = ValidarNumero(value); } }

        public Numero()
        {
            this.numero = 0;
        }

        public Numero(double numero)
        {
            this.numero = numero;
        }

        public Numero(string strNumero)
        {
            this.SetNumero = strNumero;
        }

        /// <summary>
        /// Convierte el numero de binario a decimal
        /// </summary>
        /// <param name="binario"></param>
        /// <returns></returns> El numero pasado a decimal
        public static string BinarioDecimal(string binario)
        {
            int numeroEntero = 0;
            string resultadoPasaje = "";

            for (int i = 0; i < binario.Length; i++)
            {
                if (binario[i].ToString() == "1" || binario[i].ToString() == "0")
                {
                    numeroEntero = numeroEntero + (int.Parse(binario[i].ToString()) * (int)Math.Pow(2, binario.Length - (i + 1)));
                    resultadoPasaje = numeroEntero.ToString();
                }
                else
                {
                    resultadoPasaje = "Valor invalido";
                    break;
                }
            }

            return resultadoPasaje;
        }

        /// <summary>
        /// Convierte el numero de decimal a binario
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns> El numero pasado a binario
        public static string DecimalBinario(double numero)
        {
            string resultadoPasaje = "";

            if (numero >= 0)
            {
                if (numero != 0 && numero != 1)
                {
                    resultadoPasaje = resultadoPasaje + Numero.DecimalBinario(numero / 2);
                    resultadoPasaje = resultadoPasaje + (numero % 2);
                }
                else
                {
                    resultadoPasaje = resultadoPasaje + numero;
                }
            }

            return resultadoPasaje;
        }

        /// <summary>
        /// Recibe un string y convierte el numero de decimal a binario
        /// </summary>
        /// <param name="numeroStr"></param>
        /// <returns></returns> El string parseado y pasado a binario
        public static string DecimalBinario(string numeroStr)
        {
            double numeroPasaje;
            string resultadoPasaje = "";

            if (double.TryParse(numeroStr, out numeroPasaje) && numeroPasaje >= 0)
            {
                if (numeroPasaje != 0 && numeroPasaje != 1)
                {
                    resultadoPasaje = resultadoPasaje + Numero.DecimalBinario(((int)numeroPasaje / 2).ToString());
                    resultadoPasaje = resultadoPasaje + (numeroPasaje % 2);
                }
                else
                {
                    resultadoPasaje = resultadoPasaje + numeroPasaje;
                }
            }
            else
            {
                resultadoPasaje = "Valor Invalido";
            }
            return resultadoPasaje;
        }

        /// <summary>
        /// Hace la operacion de suma
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static double operator +(Numero n1, Numero n2)
        {
            double resultadoSuma = 0;

            resultadoSuma = n1.numero + n2.numero;

            return resultadoSuma;
        }

        /// <summary>
        /// Hace la operacion de resta
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static double operator -(Numero n1, Numero n2)
        {
            double resultadoResta = 0;

            resultadoResta = n1.numero - n2.numero;

            return resultadoResta;
        }

        /// <summary>
        /// Hace la operacion de multiplicar
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static double operator *(Numero n1, Numero n2)
        {
            double resultadoMultiplicacion = 0;

            resultadoMultiplicacion = n1.numero * n2.numero;

            return resultadoMultiplicacion;
        }

        /// <summary>
        /// Hace la operacion de dividir
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static double operator /(Numero n1, Numero n2)
        {
            double resultadoDivision = 0;

            resultadoDivision = n1.numero / n2.numero;

            return resultadoDivision;
        }

        /// <summary>
        /// Valida que el numero sea numerico
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        private double ValidarNumero(string numero)
        {
            double value = 0;
            double numeroBuffer;

            if (double.TryParse(numero, out numeroBuffer))
            {
                value = numeroBuffer;
            }

            return value;
        }
    }
}
