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
    public partial class Reparos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Procedimiento llama a llenar las tres tablas modificadas en las otras paginas
                LlenarTabla();
            }
        }

        protected void LlenarTabla()
        {
            string constr = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM reparaciones"))
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
            tCodigo.Text = string.Empty;
            tFecha.Text = string.Empty;
            tEstado.Text = string.Empty;
        }

        protected void BttAgregar_Click(object sender, EventArgs e)
        {
            if (dropEquipo.Text.Length == 0 || tEstado.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.Reparo.Agregar(int.Parse(dropEquipo.Text), DateTime.Parse(tFecha.Text), tEstado.Text) > 0)
            {
                //Modificar para que DateTime se pueda enviar default si no hay dato agregado
                LlenarTabla();
                Alerta("Reparo Agregado");
                Limpiar();
            }
            else
            {
                Alerta("Error al ingresar reparo");
            }
        }
        /*
        protected void BttBorrar_Click(object sender, EventArgs e)
        {
            if (tCodigo.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.Reparo.Borrar(int.Parse(tCodigo.Text)) > 0)
            {
                LlenarTabla();
                Alerta("Reparo Eliminado");
                Limpiar();
            }
            else
            {
                Alerta("Error al eliminar reparo");
            }
        }
        */
        protected void BttConsultar_Click(object sender, EventArgs e)
        {
            if (tCodigo.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else
            {
                int codigo = int.Parse(tCodigo.Text);
                string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM reparaciones WHERE reparacionID ='" + codigo + "'"))

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
            if (tCodigo.Text.Length == 0 || dropEquipo.Text.Length == 0 || tEstado.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.Reparo.Modificar(int.Parse(tCodigo.Text), int.Parse(dropEquipo.Text), DateTime.Parse(tFecha.Text), tEstado.Text) > 0)
            {
                LlenarTabla();
                Alerta("Reparo Modificado");
                Limpiar();
            }
            else
            {
                Alerta("Error al ingresar reparo");
            }
        }

        protected void BttLlenar_Click(object sender, EventArgs e)
        {
            LlenarTabla();
        }
    }
}