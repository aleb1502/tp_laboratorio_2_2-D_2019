using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Excepciones;

namespace Archivos
{
    public class Xml<T> : IArchivo<T>
    {
        /// <summary>
        /// Metodo que guarda el XML
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool Guardar(string archivo, T datos)
        {
            bool value = false;

            XmlTextWriter writer = new XmlTextWriter(archivo, Encoding.UTF8); 
            XmlSerializer ser = new XmlSerializer(typeof(T));

            try
            {

                ser.Serialize(writer, datos);
                value = true;

            }
            catch (Exception e)
            {
                value = false;
                throw new ArchivosException(e);
            }
            finally
            {
                writer.Close();
            }

            return value;
        }

        /// <summary>
        /// Metodo que lee el archivo
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="datos"></param>
        /// <returns></returns>
        public bool Leer(string archivo, out T datos)
        {
            bool value = false;

            XmlTextReader reader = new XmlTextReader(archivo);
            XmlSerializer ser = new XmlSerializer(typeof(T));

            try
            {
                datos = (T)ser.Deserialize(reader);
                value = true;
            }
            catch (Exception e)
            {
                value = false;
                throw new ArchivosException(e);
            }
            finally
            {
                reader.Close();
            }

            return value;
        }
    }
}
