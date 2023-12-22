using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using INFO_104_Proyecto2_Grupo3.clases;
using System.Drawing;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace INFO_104_Proyecto2_Grupo3.asp
{    //INFO-104. Proyecto 2. Grupo 3.
    public partial class Filtro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void BuscarFiltro(int codigo, string filtro)
        {
            
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand(filtro, con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.Add(new SqlParameter("@codigo", codigo));

                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        datagrid.DataSource = dt;
                        datagrid.DataBind();  // Refrescar los datos
                    }
                }
            }
        }

        protected void BttConsultar_Click(object sender, EventArgs e)
        {
            if(tCodigo.Text.Length == 0) 
            {
                Alerta("No se puede ejecutar el filtro debido a que no hay un codigo escrito");
            }
            else if (ValidarNumero(tCodigo.Text))
            {
                Alerta("Error. Codigo escrito contiene caracteres no validos");
                tCodigo.Text = string.Empty;
            }
            else
            {
                int codigo = int.Parse(tCodigo.Text);
                string filtro = dropFiltro.Text;
                BuscarFiltro(codigo, filtro);
            }
        }

        public bool ValidarNumero(string numero)
        {
            return Regex.IsMatch(numero, "[^0-9]");
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
    }
}