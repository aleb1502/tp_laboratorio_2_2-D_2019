using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class NacionalidadInvalidaException : Exception
    {
        /// <summary>
        /// Llama al de base
        /// </summary>
        public NacionalidadInvalidaException() : base()
        {

        }

        /// <summary>
        /// Llama al de base para lanzar el mensaje
        /// </summary>
        public NacionalidadInvalidaException(string mensaje) : base(mensaje)
        {

        }
    }
}
