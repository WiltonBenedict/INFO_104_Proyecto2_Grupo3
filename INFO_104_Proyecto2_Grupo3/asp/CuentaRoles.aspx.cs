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
    public partial class CuentaRoles : System.Web.UI.Page
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
            //Codigo visto en clase para rellenar el datagrid de datos
            string constr = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM cuentaRol"))
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
            tFecha.Text = string.Empty;
        }

        protected void BttAgregar_Click(object sender, EventArgs e)
        {
            if (dropCuenta.Text.Length == 0 || dropCuenta.Text.Length == 0 || tFecha.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.CuentaRol.Agregar(int.Parse(dropCuenta.Text), int.Parse(dropRol.Text), DateTime.Parse(tFecha.Text)) > 0)
            {
                //Modificar para que DateTime se pueda enviar default si no hay dato agregado
                LlenarTabla();
                Alerta("Vinculacion Agregados");
                Limpiar();
            }
            else
            {
                Alerta("Error al ingresar vinculacion");
            }
        }

        protected void BttBorrar_Click(object sender, EventArgs e)
        {
            if (tCodigo.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.CuentaRol.Borrar(int.Parse(tCodigo.Text)) > 0)
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

        protected void BttConsultar_Click(object sender, EventArgs e)
        {
            if (tCodigo.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else
            {
                int codigo = int.Parse(tCodigo.Text);
                string constr = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM cuentaRol WHERE id ='" + codigo + "'"))

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
            if (tCodigo.Text.Length == 0 || dropCuenta.Text.Length == 0 || dropCuenta.Text.Length == 0 || tFecha.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.CuentaRol.Modificar(int.Parse(tCodigo.Text), int.Parse(dropCuenta.Text), int.Parse(dropRol.Text), DateTime.Parse(tFecha.Text)) > 0)
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