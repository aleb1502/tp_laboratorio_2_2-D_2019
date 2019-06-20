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
        /// Propiedad Paquetes
        /// </summary>
        public List<Paquete> Paquetes { get {return this.paquetes; } set {this.paquetes = value; } }

        /// <summary>
        /// Constructor de Correo
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
            foreach (Thread t in this.mockPaquetes)
            {
                if(t.IsAlive)
                {
                    t.Abort();
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
            StringBuilder sb = new StringBuilder();

            foreach (Paquete paquete in ((Correo)elemento).Paquetes)
            {
                sb.AppendLine(String.Format("{0} para {1} ({2})", paquete.TrackingID, paquete.DireccionEntrega, paquete.Estado.ToString()));
            }

            return sb.ToString();
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
            Thread hilo = null;
            Correo auxCorreo = new Correo();

            try
            {
                foreach (Paquete paquete in c.Paquetes)
                {
                    if (paquete == p)
                    {
                        flag = 1;
                        throw new TrackingIdRepetidoException("El producto esta repetido, no se agregó");
                        
                    }
                }

                if (flag == 0)
                {
                    c.Paquetes.Add(p);
                    hilo = new Thread(p.MockCicloDeVida);
                    c.mockPaquetes.Add(hilo);
                    hilo.Start();
                }

                auxCorreo = c;
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
