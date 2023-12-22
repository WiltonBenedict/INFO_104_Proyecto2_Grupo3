using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace INFO_104_Proyecto2_Grupo3.asp
{//INFO-104. Proyecto 2. Grupo 3.
    public partial class Roles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Llena la tabla de datos al recargar
                LlenarTabla();
            }
        }

        protected void LlenarTabla()
        {
            //Codigo visto en clase para rellenar el datagrid de datos
            string constr = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM roles"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            datagrid.DataSource = dt;
                            datagrid.DataBind();  // actualiza el grid view
                        }
                    }
                }
            }
        }

        public void Alerta(String texto)
        {
            //Codigo de alerta visto en clase para enviar alertas
            string message = texto;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        public void Limpiar()
        {
            //Este codigo se llama cada vez que un comando es ejecutado correctamente para limpiar los datos en los campos de texto
            tCodigo.Text = string.Empty;
            tDescripcion.Text = string.Empty;
        }

        protected void BttAgregar_Click(object sender, EventArgs e)
        {
            if (tDescripcion.Text.Length == 0)
            {
                //Aparece para evitar valores vacios
                Alerta("Faltan datos");
            }

            else if (clases.Rol.Agregar(tDescripcion.Text) > 0)
            {
                //Refresca la tabla y indica que se ejecuto el comando
                LlenarTabla();
                Alerta("Rol Agregado");
                Limpiar();
            }
            else
            {
                //No se borran los datos de los campos de texto y envia un mensaje de alerta
                Alerta("Error al ingresar Rol");
            }
        }

        protected void BttBorrar_Click(object sender, EventArgs e)
        {
            if (tCodigo.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.Rol.Borrar(int.Parse(tCodigo.Text)) > 0)
            {
                LlenarTabla();
                Alerta("Rol Eliminado");
                Limpiar();
            }
            else
            {
                Alerta("Error al eliminar rol");
            }
        }

        protected void BttConsultar_Click(object sender, EventArgs e)
        {
            if (tCodigo.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else
            {
                //Codigo que hace la consulta 
                int codigo = int.Parse(tCodigo.Text);
                string constr = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM roles WHERE id ='" + codigo + "'"))


                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            datagrid.DataSource = dt;
                            datagrid.DataBind();  // actualizar el grid view
                        }
                    }
                }
            }

        }

        protected void BttModificar_Click(object sender, EventArgs e)
        {
            if (tCodigo.Text.Length == 0 || tDescripcion.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.Rol.Modificar(int.Parse(tCodigo.Text), tDescripcion.Text) > 0)
            {
                LlenarTabla();
                Alerta("Cuenta Modificado");
                Limpiar();
            }
            else
            {
                Alerta("Error al modificar usuario");
            }
        }

        protected void BttLlenar_Click(object sender, EventArgs e)
        {
            //Boton para refrescar la pagina ya que la consulta puede hacer que la tabla no muestre todos los datos
            LlenarTabla();
        }
    }
}