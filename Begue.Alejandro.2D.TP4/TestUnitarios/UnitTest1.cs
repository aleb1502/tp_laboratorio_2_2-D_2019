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
            Correo c = new Correo();

            object lista = c.Paquetes;

            Assert.IsNotNull(lista);
        }

        /// <summary>
        /// Testea que los paquetes no tengan el mismo tracking
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

                Assert.Fail("No se lanzo la excepcion");
            }
            catch (Exception e)
            {

                Assert.IsInstanceOfType(e,typeof(TrackingIdRepetidoException));
            }

            
        }
    }
}
