using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace MainCorreo
{
    public partial class FrmPpal : Form
    {
        Paquete paquete;
        Correo correo;

        /// <summary>
        /// Constructor del formulario principal
        /// </summary>
        public FrmPpal()
        {
            InitializeComponent();
            correo = new Correo();
            correo.Paquetes = new List<Paquete>();
        }

        /// <summary>
        /// Agrega el paquete a los estados del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            paquete = new Paquete(txtDireccion.Text,mtxtTrackingID.Text);

            try
            {
                paquete.InformaEstado += paq_InformaEstado;
                correo += paquete;
            }
            catch (TrackingIdRepetidoException ex)
            {

                MessageBox.Show(ex.Message);
            }

            ActualizarEstados();
        }

        /// <summary>
        /// Informa y actualiza los estados
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void paq_InformaEstado(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                Paquete.DelegadoEstado d = new Paquete.DelegadoEstado(paq_InformaEstado);
                this.Invoke(d, new object[] { sender, e });
            }
            else
            {
                ActualizarEstados();
            }
        }

        /// <summary>
        /// Actualiza los estados y recorre la lista de pauqtes  
        /// </summary>
        private void ActualizarEstados()
        {
            lstEstadoEntregado.Text = "";
            lstEstadoEnViaje.Text = "";
            lstEstadoIngresado.Text = "";

            foreach (Paquete item in correo.Paquetes)
            {
                switch(item.Estado)
                {
                    case Paquete.EEstado.Ingresado:
                        lstEstadoIngresado.Items.Add(item);
                        break;
                    case Paquete.EEstado.EnViaje:
                        lstEstadoEnViaje.Items.Add(item);
                        break;
                    case Paquete.EEstado.Entregado:
                        lstEstadoEntregado.Items.Add(item);
                        break;
                }
            }
        }

        /// <summary>
        /// Cieera el formulario y los hilos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPpal_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.correo.FinEntregas();
        }

        /// <summary>
        /// Llama al metodo MostrarInformacion y reutiliza el codigo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            this.MostrarInformacion<List<Paquete>>((IMostrar<List<Paquete>>)correo);
        }

        /// <summary>
        /// Llama al metodo MostrarInformacion y reutiliza el codigo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MostrarInformacion<Paquete>((IMostrar<Paquete>)lstEstadoEntregado.SelectedItem);
        }

        /// <summary>
        /// Pregunta si el elemento es null, muestra en el richTextBox y guarda en el archivo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elemento"></param>
        private void MostrarInformacion<T>(IMostrar<T> elemento)
        {
            if(elemento != null)
            {
                rtbMostrar.Text = elemento.MostrarDatos(elemento);
                elemento.MostrarDatos(elemento).Guardar("salida");
            }
        }
    }
}
