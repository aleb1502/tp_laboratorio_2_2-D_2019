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
            bool value = false;
          
            try
            {
                string sql = "INSERT INTO Paquetes (direccionEntrega, trackingID, alumno) VALUES('" + p.DireccionEntrega + "','" + p.TrackingID + "','Begue Alejandro')";

                value = EjecutarNonQuery(sql);
            }
            catch (SqlException e)
            {
                value = false;
                throw e;
            }

            return value;
        }

        /// <summary>
        /// Constructor de clase
        /// </summary>
        static PaqueteDAO()
        {
            // CREO UN OBJETO SQLCONECTION
            conexion = new SqlConnection("Data Source=PCALEB;Initial Catalog=correo-sp-2017;Integrated Security=True");

            //this._conexion.Open();

            // CREO UN OBJETO SQLCOMMAND
            comando = new SqlCommand();
            // INDICO EL TIPO DE COMANDO
            comando.CommandType = System.Data.CommandType.Text;
            // ESTABLEZCO LA CONEXION
            comando.Connection = conexion;
        }

        /// <summary>
        /// Metodo privado que ejecuta las consultas
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private static bool EjecutarNonQuery(string sql)
        {
            bool todoOk = false;
            try
            {
                // LE PASO LA INSTRUCCION SQL
                comando.CommandText = sql;

                // ABRO LA CONEXION A LA BD
                conexion.Open();

                // EJECUTO EL COMMAND
                comando.ExecuteNonQuery();

                todoOk = true;
            }
            catch (Exception)
            {
                todoOk = false;
            }
            finally
            {
                if (todoOk)
                    conexion.Close();
            }
            return todoOk;
        }
    }
}
