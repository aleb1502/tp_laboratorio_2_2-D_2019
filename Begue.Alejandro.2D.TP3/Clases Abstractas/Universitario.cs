using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesAbstractas
{
    public abstract class Universitario : Persona
    {
        private int legajo;

        /// <summary>
        /// Sobreescritura del metodo Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool value = false;

            if(obj is Universitario && this == (Universitario)obj)
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
        /// Sobrescritura de MostrarDatos
        /// </summary>
        /// <returns></returns>
        protected virtual string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(base.ToString());
            sb.AppendFormat("\nLEGAJO NÚMUERO: {0}", this.legajo);

            return sb.ToString();
        }

        /// <summary>
        /// Operador distinto entre pg1 y pg2
        /// </summary>
        /// <param name="pg1"></param>
        /// <param name="pg2"></param>
        /// <returns></returns>
        public static bool operator !=(Universitario pg1, Universitario pg2)
        {
            return !(pg1 == pg2);
        }

        /// <summary>
        /// Operador igual igual entre pg1 y pg2
        /// </summary>
        /// <param name="pg1"></param>
        /// <param name="pg2"></param>
        /// <returns></returns>
        public static bool operator == (Universitario pg1, Universitario pg2)
        {
            bool value = false;

            if(pg1.GetType() == pg2.GetType() && (pg1.legajo == pg2.legajo || pg1.DNI == pg2.DNI))
            {
                value = true;
            }

            return value;
        }

        /// <summary>
        /// Metodo abtracto que se debera sobreescribir en las clases derivadas
        /// </summary>
        /// <returns></returns>
        protected abstract string ParticiparEnClase();

        /// <summary>
        /// Constrctor por defecto
        /// </summary>
        public Universitario()
        {

        }

        /// <summary>
        /// Constructor de Universitario que llama al de base
        /// </summary>
        /// <param name="legajo"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        public Universitario(int legajo, string nombre, string apellido, string dni, ENacionalidad nacionalidad) : base(nombre,apellido,dni,nacionalidad)
        {
            this.legajo = legajo;
        }
    }
}
