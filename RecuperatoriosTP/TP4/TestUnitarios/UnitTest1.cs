using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entidades;

namespace TestUnitarios
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test que verifica que la lista de paquetes de correo este instanciada
        /// </summary>
        [TestMethod]
        public void ListaDePaquetes()
        {
            Correo c = new Correo(); //Inicializa el constructor

            object lista = c.Paquetes; //Asigna la lista de paquetes al objeto

            Assert.IsNotNull(lista); //Usa el Assert para verificar que la lista de paquetes este instanciada
        }

        /// <summary>
        /// Testea que dos paquetes no tengan el mismo tracking
        /// </summary>
        [TestMethod]
        public void TestPaquetesNoRepetidos()
        {
            try
            {
                Correo c = new Correo();

                Paquete p1 = new Paquete("Buenos Aires", "000111");
                Paquete p2 = new Paquete("Quilmes", "000111");

                c += p1; 
                c += p2;

                Assert.Fail("No se lanzo la excepcion"); //Verifica que no se pudo lanzar la excepcion
            }
            catch (Exception e)
            {
                //Usa el assert para verificar que se lanzo la excepcion al agregar dos paquetes con el mismo tracking
                Assert.IsInstanceOfType(e, typeof(TrackingIdRepetidoException));
            }


        }
    }
}
