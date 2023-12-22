using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace INFO_104_Proyecto2_Grupo3.clases
{
    public class DBconn
    {//INFO-104. Proyecto 2. Grupo 3.
        public static SqlConnection ObtenerConexion()
        {
            string s = System.Configuration.ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            SqlConnection conexion = new SqlConnection(s);
            conexion.Open();
            return conexion;
        }
    }
}