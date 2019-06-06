using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Excepciones;

namespace EntidadesAbstractas
{
    public abstract class Persona
    {
        public enum ENacionalidad
        {
            Argentino,
            Extranjero
        }

        private string apellido;
        private int dni;
        private ENacionalidad nacionalidad;
        private string nombre;

        /// <summary>
        /// Propiedad Apellido
        /// </summary>
        public string Apellido {
            get {return this.apellido; }
            set {
                this.apellido = ValidarNombreApellido(value);
            }
        }

        /// <summary>
        /// Propiedad DNI
        /// </summary>
        public int DNI { get {return this.dni; }
            set {
                this.dni = ValidarDni(this.nacionalidad, value);
            }
        }

        /// <summary>
        /// Propiedad Nacionalidad
        /// </summary>
        public ENacionalidad Nacionalidad { get { return this.nacionalidad; } set { this.nacionalidad = value; } }

        /// <summary>
        /// Propiedad Nombre
        /// </summary>
        public string Nombre { get {return this.nombre; }
            set {
                this.nombre = ValidarNombreApellido(value);
            }
        }

        /// <summary>
        /// Propiedad StringToDNI
        /// </summary>
        public string StringToDNI {
            set {
                this.dni = ValidarDni(this.nacionalidad, value); 
            }
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Persona()
        {

        }

        /// <summary>
        /// Constructor de Persona
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="nacionalidad"></param>
        public Persona(string nombre, string apellido, ENacionalidad nacionalidad)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Nacionalidad = nacionalidad;
        }

        /// <summary>
        /// Constructor de Persona que llama al anterior
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        public Persona(string nombre, string apellido, int dni, ENacionalidad nacionalidad) : this(nombre,apellido,nacionalidad)
        {
            this.DNI = dni;
        }

        /// <summary>
        /// Constructor de Persona que llama al anterior
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        public Persona(string nombre, string apellido, string dni, ENacionalidad nacionalidad) : this(nombre, apellido, nacionalidad)
        {
            this.StringToDNI = dni;
        }

        /// <summary>
        /// Sobreescritura del metodo ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("\nNOMBRE COMPLETO: " + this.Apellido + ", " + this.Nombre);
            sb.AppendLine("\nNACIONALIDAD: " +  this.Nacionalidad);

            return sb.ToString();
        }

        /// <summary>
        /// Metodo que valida el Dni numericamente
        /// </summary>
        /// <param name="nacionalidad"></param>
        /// <param name="dato"></param>
        /// <returns></returns>
        private static int ValidarDni(ENacionalidad nacionalidad, int dato)
        {
            if (dato < 1 || dato > 99999999) 
            {
                throw new DniInvalidoException("DNI en rango inválido.");
            }

            switch (nacionalidad)
            {
                case ENacionalidad.Argentino:
                    if(dato > 89999999)
                    {
                        throw new NacionalidadInvalidaException("La nacionalidad no se coincide con el número de DNI");
                    }
                    
                    break;
                case ENacionalidad.Extranjero:
                    if (dato <= 89999999)
                    {
                        throw new NacionalidadInvalidaException("La nacionalidad no se coincide con el número de DNI");
                    }
                    
                    break;
            }

            return dato;
        }

        /// <summary>
        /// Metodo que valida el caracter del DNI
        /// </summary>
        /// <param name="nacionalidad"></param>
        /// <param name="dato"></param>
        /// <returns></returns>
        private static int ValidarDni(ENacionalidad nacionalidad, string dato)
        {
            int dniEntero;

            if (Regex.IsMatch(dato, @"^[0-9]+[0-9\.]*$")) 
            {
                dato = dato.Replace(".", ""); 
                Int32.TryParse(dato, out dniEntero); 
            }
            else 
            {
                throw new DniInvalidoException("DNI ingresado tiene un formato inválido.");
            }

            return ValidarDni(nacionalidad, dniEntero);
        }

        /// <summary>
        /// Valida que el nombre o apellido sea un caracter
        /// </summary>
        /// <param name="dato"></param>
        /// <returns></returns>
        private static string ValidarNombreApellido(string dato)
        {
            string nombrePersona = "";

            if (Regex.IsMatch(dato, @"^([a-zA-Záéíóú]+)(\s[a-zA-Záéíóú]+)*$"))
            {
                nombrePersona = dato;
            }

            return nombrePersona;
        }
    }
}
