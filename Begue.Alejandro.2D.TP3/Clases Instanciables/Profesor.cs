using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;

namespace Clases_Instanciables
{
    public sealed class Profesor : Universitario
    {
        private Queue<Universidad.EClases> clasesDelDía;
        private static Random random;

        /// <summary>
        /// Sobreescritura del metodo Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool value = false;

            if (obj is Profesor && this == (Profesor)obj)
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
        /// Random de clases
        /// </summary>
        private void _randomClases()
        {
            this.clasesDelDía.Enqueue((Universidad.EClases)random.Next(0,3));
            this.clasesDelDía.Enqueue((Universidad.EClases)random.Next(0,3));
        }

        /// <summary>
        /// Sobreescritura de MostrarDatos
        /// </summary>
        /// <returns></returns>
        protected override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(base.MostrarDatos());
            sb.AppendFormat(this.ParticiparEnClase());

            return sb.ToString();
        }

        /// <summary>
        /// Operador != entre profesor y clase que reutiliza el ==
        /// </summary>
        /// <param name="i"></param>
        /// <param name="clases"></param>
        /// <returns></returns>
        public static bool operator !=(Profesor i, Universidad.EClases clases)
        {
            return !(i == clases);
        }

        /// <summary>
        /// Operador == entre profesor y clase
        /// </summary>
        /// <param name="profesor"></param>
        /// <param name="clases"></param>
        /// <returns></returns>
        public static bool operator == (Profesor profesor, Universidad.EClases clases)
        {
            bool value = false;

            foreach (Universidad.EClases item in profesor.clasesDelDía)
            {
                if(item == clases)
                {
                    value = true;
                    break;
                }
            }

            return value;
        }

        /// <summary>
        /// Sobreescritura de ParticiparEnClase
        /// </summary>
        /// <returns></returns>
        protected override string ParticiparEnClase()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("\nCLASES DEL DÍA: ");

            foreach (Universidad.EClases item in this.clasesDelDía)
            {
                sb.AppendLine("\n" + item.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Profesor()
        {

        }

        /// <summary>
        /// Constructor de Clase
        /// </summary>
        static Profesor()
        {
            random = new Random();
        }

        /// <summary>
        /// Constructor de Profesor que llama al constructor base
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        public Profesor(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad) : base(id,nombre,apellido,dni,nacionalidad)
        {
            this.clasesDelDía = new Queue<Universidad.EClases>();
            this._randomClases();
        }

        /// <summary>
        /// Sobreescritura del metodo ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.MostrarDatos();
        }
    }
}
