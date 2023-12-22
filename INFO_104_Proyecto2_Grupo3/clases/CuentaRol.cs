using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace INFO_104_Proyecto2_Grupo3.clases
{//INFO-104. Proyecto 2. Grupo 3.
    public class CuentaRol
    {
        public int Id { get; set; }
        public int IdCuenta { get; set; }
        public int IdRol { get; set; }
        public DateTime Fecha { get; set; }

        public CuentaRol(int idCuenta, int idRol, DateTime fecha)
        {
            IdCuenta = idCuenta;
            IdRol = idRol;
            Fecha = fecha;
        }

        public CuentaRol() { }

        public static int Agregar(int IdCuenta, int IdRol, DateTime Fecha)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("agregarCuentaRol", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@idCuenta", IdCuenta));
                    cmd.Parameters.Add(new SqlParameter("@idRol", IdRol));
                    cmd.Parameters.Add(new SqlParameter("@fecha", Fecha));

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

        public static int Borrar(int Id)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("borrarCuentaRol", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", Id));

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

        public static int Modificar(int Id, int IdCuenta, int IdRol, DateTime Fecha)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("modificarCuentaRol", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@id", Id));
                    cmd.Parameters.Add(new SqlParameter("@idCuenta", IdCuenta));
                    cmd.Parameters.Add(new SqlParameter("@idRol", IdRol));
                    cmd.Parameters.Add(new SqlParameter("@fecha", Fecha));

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