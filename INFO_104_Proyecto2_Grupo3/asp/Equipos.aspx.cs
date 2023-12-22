using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace INFO_104_Proyecto2_Grupo3.asp
{    //INFO-104. Proyecto 2. Grupo 3.
    public partial class Equipos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarTabla();
            }
        }

        protected void LlenarTabla()
        {
            string constr = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM equipos"))
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

        public bool ValidarNumero(string numero)
        {
            return Regex.IsMatch(numero, "[^0-9]");
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
            tTipo.Text = string.Empty;
            tModelo.Text = string.Empty;
        }

        protected void BttAgregar_Click(object sender, EventArgs e)
        {
            if (tTipo.Text.Length == 0 || tModelo.Text.Length == 0 || dropUsuario.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.Equipo.Agregar(tTipo.Text, tModelo.Text, int.Parse(dropUsuario.Text)) > 0)
            {
                LlenarTabla();
                Alerta("Equipo Agregado");
                Limpiar();
            }
            else
            {
                Alerta("Error al ingresar equipo");
            }
        }

        protected void BttBorrar_Click(object sender, EventArgs e)
        {
            if (tCodigo.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (ValidarNumero(tCodigo.Text))
            {
                Alerta("Codigo Ingresado Invalido");
            }
            else if (clases.Equipo.Borrar(int.Parse(tCodigo.Text)) > 0)
            {
                LlenarTabla();
                Alerta("Equipo Eliminado");
                Limpiar();
            }
            else
            {
                Alerta("Error al eliminar equipo");
            }
        }

        protected void BttConsultar_Click(object sender, EventArgs e)
        {
            if (tCodigo.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (ValidarNumero(tCodigo.Text))
            {
                Alerta("Codigo Ingresado Invalido");
            }
            else
            {
                int codigo = int.Parse(tCodigo.Text);
                string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM equipos WHERE equipoID ='" + codigo + "'"))


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
            if (tCodigo.Text.Length == 0 || tTipo.Text.Length == 0 || tModelo.Text.Length == 0 || dropUsuario.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.Equipo.Modificar(int.Parse(tCodigo.Text), tTipo.Text, tModelo.Text, int.Parse(dropUsuario.Text)) > 0)
            {
                LlenarTabla();
                Alerta("Equipo Modificado");
                Limpiar();
            }
            else
            {
                Alerta("Error al modificar equipo");
            }
        }

        protected void BttLlenar_Click(object sender, EventArgs e)
        {
            LlenarTabla();
        }
    }
}