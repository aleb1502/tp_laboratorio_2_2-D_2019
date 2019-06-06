using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesAbstractas;

namespace Clases_Instanciables
{
    public sealed class Alumno : Universitario
    {
        public enum EEstadoCuenta
        {
            AlDia,
            Deudor,
            Becado
        }

        private Universidad.EClases claseQueToma;
        private EEstadoCuenta estadoCuenta;

        /// <summary>
        /// Sobreescritura del metodo Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool value = false;

            if (obj is Alumno && this == (Alumno)obj)
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
        /// Constructor por defecto
        /// </summary>
        public Alumno()
        {

        }

        /// <summary>
        /// Constructor de Alumno que llama al constructor base
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        /// <param name="claseQueToma"></param>
        public Alumno(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad, Universidad.EClases claseQueToma) : base(id,nombre,apellido,dni,nacionalidad)
        {
            this.claseQueToma = claseQueToma;
        }

        /// <summary>
        /// Constructor de Alumno que reutiliza el anterior
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        /// <param name="claseQueToma"></param>
        /// <param name="estadoCuenta"></param>
        public Alumno(int id, string nombre, string apellido, string dni, ENacionalidad nacionalidad, Universidad.EClases claseQueToma, EEstadoCuenta estadoCuenta) : this(id,nombre,apellido,dni,nacionalidad,claseQueToma)
        {
            this.estadoCuenta = estadoCuenta;
        }

        /// <summary>
        /// Sobreescritura de MostrarDatos
        /// </summary>
        /// <returns></returns>
        protected override string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(base.MostrarDatos());

            if (this.estadoCuenta == EEstadoCuenta.AlDia)
            {
                sb.AppendLine("\nESTADO DE CUENTA: Cuota al día");
            }
            else
            {
                sb.AppendFormat("\nESTADO DE CUENTA: {0}" , this.estadoCuenta);
            }

            sb.AppendFormat(this.ParticiparEnClase());

            return sb.ToString();
        }

        /// <summary>
        /// Operador != entre alumno y la clase que reutiliza el ==
        /// </summary>
        /// <param name="a"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static bool operator !=(Alumno a, Universidad.EClases clase)
        {
            return !(a == clase);
        }

        /// <summary>
        /// Operador == entre alumno y la clase
        /// </summary>
        /// <param name="a"></param>
        /// <param name="clase"></param>
        /// <returns></returns>
        public static bool operator == (Alumno a, Universidad.EClases clase)
        {
            bool value = false;

            if(a.claseQueToma == clase && a.estadoCuenta != EEstadoCuenta.Deudor)
            {
                value = true;
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

            sb.AppendFormat("\nTOMA CLASE DE {0}\r\n", this.claseQueToma);

            return sb.ToString();
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
