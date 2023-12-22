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
{    //INFO-104. Proyecto 2. Grupo 3.
    public partial class Detalles : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM detallesReparacion"))
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
            tDescripcion.Text = string.Empty;
            tFechaInicio.Text = string.Empty;
            tFechaFin.Text = string.Empty;
        }

        protected void BttAgregar_Click(object sender, EventArgs e)
        {
            if (dropReparo.Text.Length == 0 || tDescripcion.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.Detalle.Agregar(int.Parse(dropReparo.Text), tDescripcion.Text, DateTime.Parse(tFechaInicio.Text), DateTime.Parse(tFechaFin.Text)) > 0)
            {
                //Modificar para que DateTime se pueda enviar default si no hay dato agregado
                LlenarTabla();
                Alerta("Detalles Agregados");
                Limpiar();
            }
            else
            {
                Alerta("Error al ingresar Detalles");
            }
        }
        /*
        protected void BttBorrar_Click(object sender, EventArgs e)
        {
            if (tCodigo.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.Detalle.Borrar(int.Parse(tCodigo.Text)) > 0)
            {
                LlenarTabla();
                Alerta("Detalles Eliminado");
                Limpiar();
            }
            else
            {
                Alerta("Error al eliminar Detalles");
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
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM detallesReparacion WHERE detalleID ='" + codigo + "'"))

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
            if (tCodigo.Text.Length == 0 || dropReparo.Text.Length == 0 || tDescripcion.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.Detalle.Modificar(int.Parse(tCodigo.Text), int.Parse(dropReparo.Text), tDescripcion.Text, DateTime.Parse(tFechaInicio.Text), DateTime.Parse(tFechaFin.Text)) > 0)
            {
                LlenarTabla();
                Alerta("Detalles Modificado");
                Limpiar();
            }
            else
            {
                Alerta("Error al ingresar Detalles");
            }
        }

        protected void BttLlenar_Click(object sender, EventArgs e)
        {
            LlenarTabla();
        }
    }
}