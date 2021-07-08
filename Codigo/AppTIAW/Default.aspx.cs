using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace AppTIAW
{
    public partial class index : System.Web.UI.Page
    {
        MySqlConnection cn;
        MySqlCommand cmd;
        String csql;
        MySqlDataReader reader;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void btnLogin_Click(object sender, EventArgs e)
        {
            
            
            string email = txtEmail.Text;
            bool ExisteUser = false;
            string senha = txtSenha.Text;
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["tiawconnstring"].ToString();
            cn = new MySqlConnection(connString);
            cn.Open();
            csql = "SELECT * FROM usuario WHERE us_email = @email and us_senha = @senha ";
            cmd = new MySqlCommand(csql, cn);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@senha", txtSenha.Text);
            reader = cmd.ExecuteReader();
            while (reader.HasRows && reader.Read())
            {
                Session["unome"] = reader.GetString(reader.GetOrdinal("us_nome"));
                Session["ucod"] = int.Parse(reader.GetString(reader.GetOrdinal("us_cod")));
                ExisteUser = true;

            }
            if (!reader.HasRows && !reader.Read())
            {
               // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Usuario não existe!')", true);
                lblErro.Text = "Usuário ou senha inexistentes!";
            }
            reader.Close();
            cn.Close();

            if (ExisteUser) 
            {
                Response.Redirect("/WebForm1");
            }
        }
    }
}