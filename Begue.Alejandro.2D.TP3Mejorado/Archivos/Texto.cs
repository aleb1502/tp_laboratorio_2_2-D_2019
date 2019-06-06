using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;

namespace Archivos
{
    public class Texto : IArchivo<string>
    {
        /// <summary>
        /// Guarda el archivo en texto
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool Guardar(string archivo, string datos)
        {
            bool value = false;

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(archivo, true))
                {
                    file.WriteLine(datos);
                }

                value = true;
            }
            catch (Exception e)
            {
                value = false;
                throw new ArchivosException(e);
            }

            return value;
        }

        /// <summary>
        /// Lee el archivo de texto
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool Leer(string archivo, out string datos)
        {
            bool value = false;
            datos = "";

            try
            {
                using (System.IO.StreamReader file = new System.IO.StreamReader(archivo))
                {
                    datos = file.ReadToEnd();
                }

                value = true;
            }
            catch (Exception e)
            {
                value = false;
                throw new ArchivosException(e);
            }

            return value;
        }
    }
}
