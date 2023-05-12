using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Conexion
    {
        public static string cadena = ConfigurationManager.ConnectionStrings["cadena_conexion"].ToString();

        public static SqlConnection ObtenerConexionDeclaracion()
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = cadena;
            try
            {
                //SqlConnection.ClearAllPools();
                conexion.Open();
                return conexion;
            }
            catch (Exception ex)
            {
                //return null;
                //ServiciosLog.CrearLog(ex.Message);
                throw ex;
            }
        }

        public static SqlCommand ObtenerComando(SqlConnection conn, string sql)
        {
            return new SqlCommand(sql, conn);
        }

        public static SqlCommand ObtenerComando(SqlConnection conn, string sql, SqlTransaction tran)
        {
            return new SqlCommand(sql, conn, tran);
        }
    }

}
