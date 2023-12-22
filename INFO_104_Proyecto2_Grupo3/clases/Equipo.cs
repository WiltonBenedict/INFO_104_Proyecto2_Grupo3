using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;


namespace INFO_104_Proyecto2_Grupo3.clases
{//INFO-104. Proyecto 2. Grupo 3.
    public class Equipo
    {
        //Constructor
        public int equipoID { get; set; }
        public string tipoEquipo { get; set;}
        public string modelo { get; set; }
        public int usuarioID { get; set; }

        public Equipo(string tipoEquipo, string modelo, int usuarioID)
        {
            this.tipoEquipo = tipoEquipo;
            this.modelo = modelo;
            this.usuarioID = usuarioID;
        }
        public Equipo() { }
        //Metodos para ejecutar los comandos dentro de SQL
        public static int Agregar(string tipoEquipo, string modelo, int usuarioID)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("agregarequipo", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@tipoEquipo", tipoEquipo));
                    cmd.Parameters.Add(new SqlParameter("@modelo", modelo));
                    cmd.Parameters.Add(new SqlParameter("@usuarioID", usuarioID));

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

        public static int Borrar(int equipoID)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("borrarequipo", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@equipoID", equipoID));

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

        public static int Modificar(int equipoID, string tipoEquipo, string modelo, int usuarioID)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("modificarequipo", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@equipoID", equipoID));
                    cmd.Parameters.Add(new SqlParameter("@tipoEquipo", tipoEquipo));
                    cmd.Parameters.Add(new SqlParameter("@modelo", modelo));
                    cmd.Parameters.Add(new SqlParameter("@usuarioID", usuarioID));

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