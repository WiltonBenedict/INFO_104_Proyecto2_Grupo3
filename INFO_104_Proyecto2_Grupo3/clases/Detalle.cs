using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;


namespace INFO_104_Proyecto2_Grupo3.clases
{//INFO-104. Proyecto 2. Grupo 3.
    public class Detalle
    {
        public int DetalleID { get; set; }
        public int ReparacionID { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin {  get; set; }

        public Detalle(int reparacionID, string descripcion, DateTime fechaInicio, DateTime fechaFin)
        {
            ReparacionID = reparacionID;
            Descripcion = descripcion;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }

        public Detalle() { }

        public static int Agregar(int ReparacionID,string Descripcion, DateTime FechaInicio, DateTime FechaFin)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("agregarDetalles", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@reparacionID", ReparacionID));
                    cmd.Parameters.Add(new SqlParameter("@descripcion", Descripcion));
                    cmd.Parameters.Add(new SqlParameter("@fechaInicio", FechaInicio));
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", FechaFin));

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
        public static int Borrar(int DetalleID)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("borrarDetalles", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@detalleID", DetalleID));

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
        public static int Modificar(int DetalleID, int ReparacionID, string Descripcion, DateTime FechaInicio, DateTime FechaFin)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("modificarDetalles", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@detalleID", DetalleID));
                    cmd.Parameters.Add(new SqlParameter("@reparacionID", ReparacionID));
                    cmd.Parameters.Add(new SqlParameter("@descripcion", Descripcion));
                    cmd.Parameters.Add(new SqlParameter("@fechaInicio", FechaInicio));
                    cmd.Parameters.Add(new SqlParameter("@fechaFin", FechaFin));

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