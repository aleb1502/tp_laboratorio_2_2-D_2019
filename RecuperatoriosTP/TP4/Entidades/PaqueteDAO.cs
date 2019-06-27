using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Entidades
{
    public static class PaqueteDAO
    {
        private static SqlCommand comando;
        private static SqlConnection conexion;

        /// <summary>
        /// Inserta en la base de datos
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool Insertar(Paquete p)
        {
            bool insertar = false;
          
            try
            {
                comando.CommandText = "INSERT INTO Paquetes (direccionEntrega, trackingID, alumno) VALUES('" + p.DireccionEntrega + "','" + p.TrackingID + "','Begue Alejandro')";
                conexion.Open();
                comando.ExecuteNonQuery(); //Ejecuta las consulta
                insertar = true;
            }
            catch (SqlException e)
            {
                insertar = false;
                throw new Exception("No se pudo guardar en base de datos",e); //Lanza la excepcion
            }
            finally
            {
                if(conexion.State != System.Data.ConnectionState.Closed)
                {
                    conexion.Close();
                }
            }

            return insertar;
        }

        /// <summary>
        /// Constructor de clase
        /// </summary>
        static PaqueteDAO()
        {
            // Creo el SqlConnection
            conexion = new SqlConnection("Data Source=PCALEB;Initial Catalog=correo-sp-2017;Integrated Security=True");

            // Creo el SqlCommand
            comando = new SqlCommand();
            // Indico el tipo de comando
            comando.CommandType = System.Data.CommandType.Text;
            // Establezco la conexion
            comando.Connection = conexion;
        }

        
    }
}
