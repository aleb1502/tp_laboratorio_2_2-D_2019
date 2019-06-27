using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Entidades
{
    public static class GuardaString
    {
        /// <summary>
        /// Metodo de extension que se usa para guardar archivo de texto
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public static bool Guardar(this string texto, string archivo)
        {
            bool value = false; 
            FileStream fs = null;

            try
            {
                //Guarda el archivo en el escritorio
                archivo = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + archivo;

                //Si el archivo existe, lo va a agregar
                fs = new FileStream(archivo, FileMode.Append, FileAccess.Write);

                using (StreamWriter sw = new StreamWriter(fs))
                {   
                    //escribe en el archivo el objeto
                    sw.WriteLine(texto);
                }

                value = true;
            }
            catch (FileNotFoundException e)
            {
                value = false;
                throw new FileNotFoundException("Error en la ruta", e);
            }
            finally
            {
                if(fs != null)
                {
                    fs.Close();
                }
            }

            return value;
        }
    }
}
