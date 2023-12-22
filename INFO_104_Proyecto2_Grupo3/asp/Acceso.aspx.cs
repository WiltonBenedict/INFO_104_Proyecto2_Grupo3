using INFO_104_Proyecto2_Grupo3.clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace INFO_104_Proyecto2_Grupo3
{
    //INFO-104. Proyecto 2. Grupo 3.
    public partial class Acceso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BttIngresar_Click(object sender, EventArgs e)
        {
            Cuentas.SetCorreo(tUsuario.Text);
            Cuentas.SetClave(tClave.Text);

            if (Cuentas.ValidarAcceso() > 0)
            {
                Response.Redirect("Inicio.aspx");
            }
            else
            {
                tClave.Text = string.Empty; 
                Alerta("Datos invalidos");
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
    }
}