using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace INFO_104_Proyecto2_Grupo3.clases
{//INFO-104. Proyecto 2. Grupo 3.
    public class Reparo
    {
        public int ReparacionID {  get; set; }
        public int EquipoID { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }

        public Reparo(int equipoID, DateTime fechaSolicitud, string estado)
        {
            EquipoID = equipoID;
            FechaSolicitud = fechaSolicitud;
            Estado = estado;
        }

        public Reparo() { }

        public static int Agregar(int EquipoID, DateTime FechaSolicitud, string Estado)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("agregarReparo", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@equipoID", EquipoID));
                    cmd.Parameters.Add(new SqlParameter("@fechaSolicitud", FechaSolicitud));
                    cmd.Parameters.Add(new SqlParameter("@estado", Estado));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;
        }
/*
        public static int Borrar(int ReparacionID)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("borrarReparo", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@reparacionID", ReparacionID));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;
        }
*/
        public static int Modificar(int ReparacionID, int EquipoID, DateTime FechaSolicitud, string Estado) 
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("modificarReparo", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@reparacionID", ReparacionID));
                    cmd.Parameters.Add(new SqlParameter("@equipoID", EquipoID));
                    cmd.Parameters.Add(new SqlParameter("@fechaSolicitud", FechaSolicitud));
                    cmd.Parameters.Add(new SqlParameter("@estado", Estado));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;
        }
    }
}