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
{//INFO-104. Proyecto 2. Grupo 3.
    public partial class Usuarios : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM usuarios"))
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

        //Se necesita modificar en uno propio
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
            tNombre.Text = string.Empty;
            tCorreo.Text = string.Empty;
            tTelefono.Text = string.Empty;
        }
        protected void BttAgregar_Click1(object sender, EventArgs e) //FUNCIONAL
        {
            if (tNombre.Text.Length == 0 || tCorreo.Text.Length == 0 || tTelefono.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (clases.Usuario.Agregar(tNombre.Text, tCorreo.Text, tTelefono.Text) > 0)
            {
                LlenarTabla();
                Alerta("Usuario Agregado");
                Limpiar();
            }
            else
            {
                Alerta("Error al ingresar usuario");
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
            else if (clases.Usuario.Borrar(int.Parse(tCodigo.Text)) > 0)
            {
                LlenarTabla();
                Alerta("Usuario Eliminado");
                Limpiar();
            }
            else
            {
                Alerta("Error al ingresar usuario");
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
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM usuarios WHERE usuarioID ='" + codigo + "'"))


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
            if (tNombre.Text.Length == 0 || tCorreo.Text.Length == 0 || tTelefono.Text.Length == 0)
            {
                Alerta("Faltan datos");
            }
            else if (ValidarNumero(tCodigo.Text))
            {
                Alerta("Codigo Ingresado Invalido");
            }
            else if (clases.Usuario.Modificar(int.Parse(tCodigo.Text), tNombre.Text, tCorreo.Text, tTelefono.Text) > 0)
            {
                LlenarTabla();
                Alerta("Usuario Modificado");
                Limpiar();
            }
            else
            {
                Alerta("Error al modificar usuario");
            }
        }

        protected void BttLlenar_Click(object sender, EventArgs e)
        {
            LlenarTabla();
        }
    }
}