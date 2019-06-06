using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Excepciones;
using Archivos;

namespace Clases_Instanciables
{
    public class Universidad
    {
        public enum EClases
        {
            Programacion,
            Laboratorio,
            Legislacion,
            SPD
        }

        private List<Alumno> alumnos;
        private List<Jornada> jornada;
        private List<Profesor> profesores;

        /// <summary>
        /// Propiedad Alumnos
        /// </summary>
        public List<Alumno> Alumnos { get {return this.alumnos; } set {this.alumnos = value; } }

        /// <summary>
        /// Propiedad Instructores
        /// </summary>
        public List<Profesor> Instructores { get {return this.profesores; } set {this.profesores = value; } }

        /// <summary>
        /// Propiedad Jornadas
        /// </summary>
        public List<Jornada> Jornadas {
            get {return this.jornada; }
            set {this.jornada = value; }
        }

        /// <summary>
        /// Propiedad Jornada indexada
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Jornada this[int i] {
            get
            {
                if (i >= 0 && i < this.Jornadas.Count)
                    return this.Jornadas[i];
                else
                    return null;
            }
            set
            {
                if (i >= 0 && i < this.Jornadas.Count)
                    this.Jornadas[i] = value;
            }
        }

        /// <summary>
        /// Sobreescritura del metodo Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool value = false;

            if (obj is Universidad && this == (Universidad)obj)
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
        /// Metodo que reutiliza el Guardar de Archivos
        /// </summary>
        /// <param name="uni"></param>
        /// <returns></returns>
        public static bool Guardar(Universidad uni)
        {
            bool value = true;

            try
            {
                Xml<Universidad> xml = new Xml<Universidad>();
                if (xml.Guardar("../../../Universidad.xml", uni) == true)
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
        /// Metodo que reutiliza el Leer de Archivos
        /// </summary>
        /// <returns></returns>
        public static Universidad Leer()
        {
            Universidad uni;
            Xml<Universidad> aux = new Xml<Universidad>();
            Universidad uniLeido = null;

            try
            {
                if (aux.Leer("../../../Universidad.xml", out uni) == true)
                {
                    uniLeido = uni;
                }
            }
            catch (Exception e)
            {
                uniLeido = null;
                throw new ArchivosException(e);
            }

            return uniLeido;
        }

        /// <summary>
        /// Sobreescritura de MostrarDatos
        /// </summary>
        /// <param name="uni"></param>
        /// <returns></returns>
        private static string MostrarDatos(Universidad uni)
        {
            StringBuilder sb = new StringBuilder();

            foreach (Jornada item in uni.Jornadas)
            {
                sb.AppendFormat("JORNADA: \r\n");
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }


        /// <summary>
        /// Operador distinto entre Universidad y Alumno
        /// </summary>
        /// <param name="g"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool operator !=(Universidad g, Alumno a)
        {
            return !(g == a);
        }

        /// <summary>
        /// Operador distinto entre Universidad y Profesor
        /// </summary>
        /// <param name="g"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool operator !=(Universidad g, Profesor i)
        {
            return !(g == i);
        }

        /// <summary>
        /// Operador distinto entre Universidad y la clase
        /// </summary>
        /// <param name="u"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static Profesor operator !=(Universidad u, EClases clase)
        {
            foreach (Profesor p in u.Instructores)
            {
                if (p != clase) 
                {
                    return p;
                }
            }
            
            throw new SinProfesorException();
        }

        /// <summary>
        /// Operador que agrega alumnos a la jornada y agrega tambien jornadas
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static Universidad operator + (Universidad g, EClases clase)
        {
            Jornada jor = new Jornada(clase,g == clase);

            foreach (Alumno alumno in g.Alumnos) 
            {
                if (alumno == clase) 
                {
                    jor.Alumnos.Add(alumno); 
                }
            }

            g.Jornadas.Add(jor);

            return g;
        }

        /// <summary>
        /// Operador que agrega alumnos a la universidad
        /// </summary>
        /// <param name="u"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static Universidad operator + (Universidad u, Alumno a)
        {
            if (u != a) 
            {
                u.Alumnos.Add(a);
                return u;
            }
            else
            {
                throw new AlumnoRepetidoException();
            }
        }

        /// <summary>
        /// Operador que agrega un profesor a la universidad
        /// </summary>
        /// <param name="u"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static Universidad operator + (Universidad u, Profesor i)
        {
            short flag = 0;

            if(u == i)
            {
                flag = 1;
                return u;
            }

            if(flag == 0)
            {
                u.Instructores.Add(i);
            }

            return u;
        }

        /// <summary>
        /// Operador de igualdad entre Universidad y Alumno
        /// </summary>
        /// <param name="g"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool operator == (Universidad g, Alumno a)
        {
            bool value = false;

            foreach (Alumno item in g.Alumnos)
            {
                if(item == a)
                {
                    value = true;
                }
            }

            return value;
        }

        /// <summary>
        /// Operador de igualdad entre Universidad y Profesor
        /// </summary>
        /// <param name="g"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool operator ==(Universidad g, Profesor i)
        {
            bool value = false;

            foreach (Profesor item in g.Instructores)
            {
                if(item == i)
                {
                    value = true;
                }
            }

            return value;
        }

        /// <summary>
        /// Operador de igualdad entre Universidad y la clase
        /// </summary>
        /// <param name="u"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static Profesor operator == (Universidad u, EClases clase)
        {
            foreach (Profesor p in u.Instructores)
            {
                if (p == clase) 
                {
                    return p;
                }
            }
            
            throw new SinProfesorException();
        }

        /// <summary>
        /// Sobreescritura del metodo ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return MostrarDatos(this);
        }

        /// <summary>
        /// Constructor de Universidad
        /// </summary>
        public Universidad()
        {
            this.Alumnos = new List<Alumno>();
            this.Jornadas = new List<Jornada>();
            this.Instructores = new List<Profesor>();
        }
    }
}
