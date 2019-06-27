using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Entidades
{
    public class Paquete : IMostrar<Paquete>
    {
        public delegate void DelegadoEstado(object sender, EventArgs e);
        public event DelegadoEstado InformarEstado;

        public enum EEstado
        {
            Ingresado,
            EnViaje,
            Entregado
        }

        private string direccionEntrega;
        private EEstado estado;
        private string trackingID;

        /// <summary>
        /// Propiedad DireccionEntrega
        /// </summary>
        public string DireccionEntrega { get {return this.direccionEntrega; } set {this.direccionEntrega = value; } }

        /// <summary>
        /// Propiedad Estado
        /// </summary>
        public EEstado Estado { get { return this.estado; } set { this.estado = value; } }

        /// <summary>
        /// Propiedad TrackingID
        /// </summary>
        public string TrackingID { get { return this.trackingID; } set { this.trackingID = value; } }

        /// <summary>
        /// El ciclo de vida del producto que cambia de estado hasta ser entregado
        /// </summary>
        public void MockCicloDeVida()
        {
            bool cicloDeVida = true;

            //Lanza el evento, lo duerme 4 segundos, pasa al siguiente estado hasta llegar a ser entregado
            while (cicloDeVida == true)
            {
                InformarEstado.Invoke(this, EventArgs.Empty);
                Thread.Sleep(4000);
                if (this.Estado == EEstado.Entregado)
                {
                    break;
                }
                this.Estado++;
            }

            try
            {
                //Guarda en base de datos
                PaqueteDAO.Insertar(this);
            }
            catch (Exception e)
            {
                throw new Exception("No pudo insertar en BD",e);
            }
        }

        /// <summary>
        /// Metodo que muestra los datos
        /// </summary>
        /// <param name="elemento"></param>
        /// <returns></returns>
        public string MostrarDatos(IMostrar<Paquete> elemento)
        {
            StringBuilder mostrarDato = new StringBuilder();

            //Muestra el paquete con sus propios datos
            mostrarDato.AppendLine(string.Format("{0} para {1}\n", ((Paquete)elemento).TrackingID, ((Paquete)elemento).DireccionEntrega));

            return mostrarDato.ToString();
        }

        /// <summary>
        /// Operador distinto que reutiliza el de igualdad
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool operator !=(Paquete p1, Paquete p2)
        {
            return !(p1 == p2); //Reutiliza codigo
        }

        /// <summary>
        /// Operador de igualdad que lo hace si cumple la condicion
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool operator == (Paquete p1, Paquete p2)
        {
            bool paqueteIgual = false;

            //Pregunta si tienen el mismo Tracking
            if(p1.TrackingID == p2.TrackingID)
            {
                paqueteIgual = true;
            }

            return paqueteIgual;
        }

        /// <summary>
        /// Constrctor de paquete
        /// </summary>
        /// <param name="direccionEntrega"></param>
        /// <param name="trackingID"></param>
        public Paquete(string direccionEntrega, string trackingID)
        {
            this.DireccionEntrega = direccionEntrega;
            this.TrackingID = trackingID;
            this.Estado = EEstado.Ingresado;
        }

        /// <summary>
        /// Sobrescritura del metodo ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //Reutiliza el MostrarDatos pasandole this
            return this.MostrarDatos(this);
        }

        /// <summary>
        /// Sobrescritura del Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            bool value = false;

            //Pregunta si el objeto es del tipo Paquete
            if(obj is Paquete && this == (Paquete)obj)
            {
                value = true;
            }

            return value;
        }

        /// <summary>
        /// Sobreescritura del GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            //Retorna la base del GetHashCode
            return base.GetHashCode();
        }
    }
}
