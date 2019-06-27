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
        Paquete paqueteCorreo;
        Correo correo;

        public FrmPpal()
        {
            InitializeComponent();
            correo = new Correo();
        }

        /// <summary>
        /// Agrega el paquete a los estados del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //Instancia el paquete
            paqueteCorreo = new Paquete(txtDireccion.Text, mtxtTrackingID.Text);

            //Agrega el manejador al evento de paquete
            paqueteCorreo.InformarEstado += paq_InformaEstado;

            try
            {
                //Agrega el paquete a la lista controlando la excepcion
                correo += paqueteCorreo;
            }
            catch (TrackingIdRepetidoException ex)
            {

                MessageBox.Show(ex.Message);
            }

            //Limpia los textbox
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
                //Limpia los textbox
                ActualizarEstados();
            }
        }

        /// <summary>
        /// Actualiza los estados y recorre la lista de pauqtes  
        /// </summary>
        private void ActualizarEstados()
        {
            //Limpia los textBox
            this.lstEstadoEntregado.Items.Clear();
            this.lstEstadoEnViaje.Items.Clear();
            this.lstEstadoIngresado.Items.Clear();

            //Recorre la lista de paquetes y le asigna a cada estado un paquete
            foreach (Paquete packCorreo in correo.Paquetes)
            {
                switch (packCorreo.Estado)
                {
                    case Paquete.EEstado.Ingresado:
                        lstEstadoIngresado.Items.Add(packCorreo);
                        break;
                    case Paquete.EEstado.EnViaje:
                        lstEstadoEnViaje.Items.Add(packCorreo);
                        break;
                    case Paquete.EEstado.Entregado:
                        lstEstadoEntregado.Items.Add(packCorreo);
                        break;
                }
            }
        }

        /// <summary>
        /// Cierra el formulario y los hilos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmPpal_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Llama al metodo FinEntregas de correo
            this.correo.FinEntregas();
        }

        /// <summary>
        /// Llama al metodo MostrarInformacion y reutiliza el codigo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            //Muestra la informacion de correo
            this.MostrarInformacion<List<Paquete>>((IMostrar<List<Paquete>>)correo);
        }

        /// <summary>
        /// Pregunta si el elemento es null, muestra en el richTextBox y guarda en el archivo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elemento"></param>
        private void MostrarInformacion<T>(IMostrar<T> elemento)
        {
            string dato = "";

            //Pregunta si el elemento es nulo
            if (elemento != null)
            {
                try
                {
                    //Pregunta si el elemento es Correo o Paquete para que lo pueda guardar en el archivo
                    //y mostrarlo en el richTextBox controlando la excepcion
                    if (elemento is Correo)
                    {
                        dato = ((Correo)elemento).MostrarDatos((Correo)elemento);
                    }
                    else if (elemento is Paquete)
                    {
                        dato = ((Paquete)elemento).ToString();
                    }

                    dato.Guardar("salida.txt");
                    rtbMostrar.Text = dato;
                }
                catch (Exception exc)
                {
                    dato = "";
                    rtbMostrar.Text = "";
                    MessageBox.Show(exc.Message);
                }

            }
        }

        /// <summary>
        /// Llama al metodo MostrarInformacion y reutiliza el codigo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Muestra la informacion del paquete seleccionado que fue entregado
            this.MostrarInformacion<Paquete>((IMostrar<Paquete>)lstEstadoEntregado.SelectedItem);
        }

    }
}
