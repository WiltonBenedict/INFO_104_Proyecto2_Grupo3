using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace INFO_104_Proyecto2_Grupo3.clases
{//INFO-104. Proyecto 2. Grupo 3.
    public class Usuario
    {
        public int usuarioId { get; set; }
        public string nombre { get; set; }
        public string correoElectronico { get; set; }
        public string telefono { get; set; }

        //Constructor
        public Usuario(string nombre, string correoElectronico, string telefono)
        {
            this.nombre = nombre;
            this.correoElectronico = correoElectronico;
            this.telefono = telefono;
        }
        public Usuario() { }

        //Metodos para ejecutar los comandos dentro de SQL
        public static int Agregar(string nombre, string correoElectronico, string telefono)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("agregarusuario", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@correoElectronico", correoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@telefono", telefono));

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

        public static int Borrar(int usuarioID)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("borrarusuario", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
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

        public static int Modificar(int usuarioID, string nombre, string correoElectronico, string telefono)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBconn.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("modificarusuario", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@usuarioID", usuarioID));
                    cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
                    cmd.Parameters.Add(new SqlParameter("@correoElectronico", correoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@telefono", telefono));

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