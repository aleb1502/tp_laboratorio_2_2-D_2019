using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class ArchivosException : Exception
    {
        /// <summary>
        /// Llama al de base para lanzar el mensaje con el innerException
        /// </summary>
        /// <param name="innerException"></param>
        public ArchivosException(Exception innerException) : base(innerException.Message)
        {

        }
    }
}
