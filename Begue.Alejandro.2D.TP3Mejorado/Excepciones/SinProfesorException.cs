using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class SinProfesorException : Exception
    {
        /// <summary>
        /// Llama al de base para lanzar el mensaje
        /// </summary>
        public SinProfesorException() : base("No hay Profesor para la clase.")
        {

        }
    }
}
