using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class DniInvalidoException : Exception
    {
        private string mensajeBase;

        /// <summary>
        /// Llama al de base
        /// </summary>
        public DniInvalidoException() : base()
        {

        }

        /// <summary>
        /// Llama al de base para lanzar el mensaje desde la Exception
        /// </summary>
        /// <param name="e"></param>
        public DniInvalidoException(Exception e) : base(e.Message)
        {

        }

        /// <summary>
        /// Llama al de base para lanzar el mensaje
        /// </summary>
        /// <param name="message"></param>
        public DniInvalidoException(string message) : base(message)
        {

        }

        /// <summary>
        /// Llama al de base para lanzar el mensaje y el innerException
        /// </summary>
        /// <param name="message"></param>
        /// <param name="e"></param>
        public DniInvalidoException(string message, Exception e) : base(message,e)
        {

        }
    }
}
