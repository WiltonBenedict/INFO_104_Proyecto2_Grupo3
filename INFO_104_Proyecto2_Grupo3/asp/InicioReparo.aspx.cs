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
    public partial class InicioReparo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Procedimiento llama a llenar las tres tablas modificadas en las otras paginas
                LlenarTablaReparos();
                LlenarTablaDetalles();
                LlenarTablaAsignaciones();
            }

        }

        protected void LlenarTablaReparos()
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
                            datagrid1.DataSource = dt;
                            datagrid1.DataBind();  // actualiza el grid view
                        }
                    }
                }
            }
        }
        protected void LlenarTablaDetalles()
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
                            datagrid2.DataSource = dt;
                            datagrid2.DataBind();  // actualiza el grid view
                        }
                    }
                }
            }
        }
        protected void LlenarTablaAsignaciones()
        {
            string constr = ConfigurationManager.ConnectionStrings["conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT *  FROM asignaciones"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            datagrid3.DataSource = dt;
                            datagrid3.DataBind();  // actualiza el grid view
                        }
                    }
                }
            }
        }

        protected void BttAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioAdministrar.aspx");
        }
    }
}