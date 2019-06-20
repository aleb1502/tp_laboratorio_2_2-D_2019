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
                archivo = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//" + archivo + ".txt";

                fs = new FileStream(archivo, FileMode.Append, FileAccess.Write);

                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(texto);
                }

                value = true;
            }
            catch (ArgumentException)
            {
                value = false;
                string fallo = "Error en la ruta";
                throw new ArgumentException(fallo);
            }
            catch (FileNotFoundException)
            {
                value = false;
                string fallo = "Error en la ruta";
                throw new FileNotFoundException(fallo);
            }
            catch (DirectoryNotFoundException)
            {
                value = false;
                string fallo = "Error en la ruta";
                throw new DirectoryNotFoundException(fallo);
            }
            catch (IOException)
            {
                value = false;
                string fallo = "Archivo en uso";
                throw new IOException(fallo);
            }
            catch (NotSupportedException)
            {
                value = false;
                string fallo = "La ruta contiene caracteres no aceptados";
                throw new NotSupportedException(fallo);
            }
            catch (System.Security.SecurityException)
            {
                value = false;
                string fallo = "El usuario no tiene los permisos necesarios";
                throw new System.Security.SecurityException(fallo);
            }
            catch (InvalidOperationException)
            {
                value = false;
                string fallo = "Nombre no encontrado";
                throw new InvalidOperationException(fallo);
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
