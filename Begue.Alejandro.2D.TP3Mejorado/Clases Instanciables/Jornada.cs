using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;
using Archivos;

namespace Clases_Instanciables
{
    public class Jornada
    {
        private List<Alumno> alumnos;
        private Universidad.EClases clase;
        private Profesor instructor;

        /// <summary>
        /// Propiedad Alumnos
        /// </summary>
        public List<Alumno> Alumnos { get {return this.alumnos; } set {this.alumnos = value; } }

        /// <summary>
        /// Propiedad Clase
        /// </summary>
        public Universidad.EClases Clase { get {return this.clase; } set {this.clase = value; } }

        /// <summary>
        /// Propiedad Instructor
        /// </summary>
        public Profesor Instructor { get {return this.instructor; } set {this.instructor = value; } }

        /// <summary>
        /// Sobreescritura del metodo Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool value = false;

            if (obj is Jornada && this == (Jornada)obj)
            {
                value = true;
            }

            return value;
        }

        /// <summary>
        /// Sobreescritura de GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        /// <summary>
        /// Metodo Guardar que guarda el archivo
        /// </summary>
        /// <param name="jornada"></param>
        /// <returns></returns>
        public static bool Guardar(Jornada jornada)
        {
            bool value = false;
            Texto texto = new Texto();

            try
            {
                if (texto.Guardar("../../../Jornada.txt", jornada.ToString()) == true)
                { 
                    value = true;
                }
            }
            catch (Exception e)
            {
                value = false;
                throw new ArchivosException(e);
            }

            return value;
        }

        /// <summary>
        /// Constructor por defecto que iniclializa la lista
        /// </summary>
        private Jornada()
        {
            this.Alumnos = new List<Alumno>();
        }

        /// <summary>
        /// Constrctor que reutiliza el privado
        /// </summary>
        /// <param name="clase"></param>
        /// <param name="instructor"></param>
        public Jornada(Universidad.EClases clase, Profesor instructor) : this()
        {
            this.Clase = clase;
            this.Instructor = instructor;
        }

        /// <summary>
        /// Metodo que lee el archivo
        /// </summary>
        /// <returns></returns>
        public static string Leer()
        {
            Texto texto = new Texto();
            string datos;
            string datoLeido = "";

            try
            {
                if (texto.Leer("../../../Jornada.txt", out datos) == true)
                { 
                    datoLeido = datos;
                }
            }
            catch (Exception e)
            {
                datoLeido = "";
                throw new ArchivosException(e);
            }

            return datoLeido;
        }

        /// <summary>
        /// Operador distinto entre Jornada y Alumno que reutiliza el de igualdad
        /// </summary>
        /// <param name="j"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool operator !=(Jornada j, Alumno a)
        {
            return !(j == a);
        }

        /// <summary>
        /// Operador que agrega alumnos
        /// </summary>
        /// <param name="j"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Jornada operator + (Jornada j, Alumno a)
        {
            short flag = 0;

            if(j == a)
            {
                flag = 1;
                return j;
            }

            if(flag == 0)
            {
                j.Alumnos.Add(a);
            }

            return j;
        }

        /// <summary>
        /// Operador de igualdad entre Jornada y Alumno
        /// </summary>
        /// <param name="j"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool operator == (Jornada j, Alumno a)
        {
            bool value = false;

            foreach (Alumno item in j.Alumnos)
            {
                if(item == a)
                {
                    value = true;
                }
            }

            return value;
        }

        /// <summary>
        /// Sobreescritura del metodo ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("CLASE DE {0} POR NOMBRE COMPLETO: {1} \r\n", this.Clase, this.Instructor.ToString());

            sb.AppendFormat("ALUMNOS: ");

            foreach (Alumno item in this.Alumnos)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
    }
}
