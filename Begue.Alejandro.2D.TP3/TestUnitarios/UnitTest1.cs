using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Excepciones;
using EntidadesAbstractas;
using Clases_Instanciables;

namespace TestUnitarios
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test que valida que lanze la excepcion DniInvalidoException
        /// </summary>
        [TestMethod]
        public void TestDniInvalidoException()
        {
            try
            {
                string dni = "14abd1u7";

                Alumno a1 = new Alumno(19,"Ricardo","Centurion",dni,Persona.ENacionalidad.Argentino,Universidad.EClases.Laboratorio);

                Assert.Fail("No se lanzo la excepcion");
            }
            catch (Exception e)
            {

                Assert.IsInstanceOfType(e,typeof(DniInvalidoException));
            }
        }

        /// <summary>
        /// Test que valida que lanze la excepcion NacionalidadInvalidaException
        /// </summary>
        [TestMethod]
        public void TestNacionalidadInvalidaException()
        {
            try
            {
                string dni = "99999999";

                Profesor p1 = new Profesor(24,"Gustavo","Brugnano",dni,Persona.ENacionalidad.Argentino);

                Assert.Fail("No se lanzo la excepcion");
            }
            catch (Exception e)
            {

                Assert.IsInstanceOfType(e, typeof(NacionalidadInvalidaException));
            }
        }

        /// <summary>
        /// Test que valida que lanze la excepcion SinProfesorException
        /// </summary>
        [TestMethod]
        public void TestSinProfesorException()
        {
            try
            {
                Universidad u = new Universidad();
                Universidad.EClases clases = Universidad.EClases.Programacion;
                u.Instructores = new System.Collections.Generic.List<Profesor>();

                foreach (Profesor item in u.Instructores)
                {
                    Assert.IsFalse(item == clases);

                    Assert.Fail("No se lanzo la excepcion");
                }
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(SinProfesorException));
            }
        }

        /// <summary>
        /// Test que valida que lanze la excepcion AlumnoRepetidoException
        /// </summary>
        [TestMethod]
        public void TestAlumnoRepetidoException()
        {
            try
            {
                Universidad u = new Universidad();
                u.Alumnos = new System.Collections.Generic.List<Alumno>();
                Alumno a1 = new Alumno(24,"Fede","Lopez","14589698",Persona.ENacionalidad.Argentino,Universidad.EClases.SPD);
                Alumno a2 = new Alumno(24, "Fede", "Lopez", "14589698", Persona.ENacionalidad.Argentino, Universidad.EClases.SPD);

                u += a1;
                u += a2;

                Assert.Fail("No se lanzo la excepcion");
            }
            catch (Exception e)
            {

                Assert.IsInstanceOfType(e, typeof(AlumnoRepetidoException));
            }
        }

        /// <summary>
        /// Test que valida que lanze la excepcion DniInvalidoException
        /// </summary>
        [TestMethod]
        public void TestValorNumerico()
        {
            try
            {
                string dni = "14wed4q7";

                Profesor p1 = new Profesor(24, "Gustavo", "Brugnano", dni, Persona.ENacionalidad.Argentino);

                Assert.Fail("No se lanzo la excepcion");
            }
            catch (Exception e)
            {

                Assert.IsInstanceOfType(e, typeof(DniInvalidoException));
            }
        }

        /// <summary>
        /// Test que valida que no haya valores nulos
        /// </summary>
        [TestMethod]
        public void TestValoresNulos()
        {
            Universidad u = new Universidad();

            Assert.IsNotNull(u.Alumnos);
            Assert.IsNotNull(u.Instructores);
            Assert.IsNotNull(u.Jornadas);

        }
    }
}
