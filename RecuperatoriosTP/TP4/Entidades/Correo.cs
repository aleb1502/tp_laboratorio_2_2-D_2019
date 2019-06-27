using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Entidades
{
    public class Correo : IMostrar<List<Paquete>>
    {
        private List<Thread> mockPaquetes;
        private List<Paquete> paquetes;

        /// <summary>
        /// Propiedad de lista de paquetes
        /// </summary>
        public List<Paquete> Paquetes { get {return this.paquetes; } set {this.paquetes = value; } }

        /// <summary>
        /// Constructor de Correo que inicializa la lista de hilos y la lista de paquetes
        /// </summary>
        public Correo()
        {
            this.mockPaquetes = new List<Thread>();
            this.Paquetes = new List<Paquete>();
        }

        /// <summary>
        /// Cierra todos los hilos activos
        /// </summary>
        public void FinEntregas()
        {
            //Pregunta si la lista de hilos es nula
            if (this.mockPaquetes != null)
            {   //Recorre la lista de hilos preguntando si quedó alguno funcionando así lo cierra
                foreach (Thread hilioPaquetes in this.mockPaquetes)
                {
                    if (hilioPaquetes.IsAlive)
                    {
                        hilioPaquetes.Abort();
                    }

                }

            }
            
        }

        /// <summary>
        /// Metodo que muestra los datos
        /// </summary>
        /// <param name="elemento"></param>
        /// <returns></returns>
        public string MostrarDatos(IMostrar<List<Paquete>> elemento)
        {
            StringBuilder mostrarDato = new StringBuilder();

            //Muestra el listado de paquetes con sus propios datos
            foreach (Paquete paquete in ((Correo)elemento).Paquetes)
            {
                mostrarDato.AppendLine(String.Format("{0} para {1} ({2})", paquete.TrackingID, paquete.DireccionEntrega, paquete.Estado.ToString()));
            }

            return mostrarDato.ToString();
        }

        /// <summary>
        /// Operador que agrega paquetes al correo
        /// </summary>
        /// <param name="c"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Correo operator + (Correo c, Paquete p)
        {
            short flag = 0;
            Thread hiloPaquete = null;
            Correo auxCorreo = new Correo();

            try
            {
                //Recorre la lista preguntando si hay algun paquete repetido para que así lance la excepcion
                foreach (Paquete paqueteCorreo in c.Paquetes)
                {
                    if (paqueteCorreo == p)
                    {
                        flag = 1;
                        throw new TrackingIdRepetidoException("El producto esta repetido, no se agregó");
                        
                    }
                }

                //Agrega el paquete a la lista, inicia su ciclo de vida, y agrega a la lista de hilos
                if (flag == 0)
                {
                    c.Paquetes.Add(p);
                    hiloPaquete = new Thread(p.MockCicloDeVida);
                    c.mockPaquetes.Add(hiloPaquete);
                    hiloPaquete.Start();
                }

                auxCorreo = c; //Devuelve el correo
            }
            catch (Exception e)
            {
                flag = 1;
                throw new TrackingIdRepetidoException("El producto esta repetido, no se agregó", e);
            }

            return auxCorreo;
        }
    }
}
