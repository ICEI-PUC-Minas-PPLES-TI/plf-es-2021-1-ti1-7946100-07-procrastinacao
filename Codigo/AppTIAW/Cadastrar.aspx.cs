using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace AppTIAW
{
    public partial class _Default : Page
    {
        MySqlConnection cn;
        MySqlCommand cmd;
        String csql;
        MySqlDataReader reader;
        string username;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCadastro_Click1(object sender, EventArgs e)
        {
            
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["tiawconnstring"].ToString();
            cn = new MySqlConnection(connString);
            cn.Open();
             csql = "SELECT * FROM usuario WHERE us_email = @email ";
            cmd = new MySqlCommand(csql, cn);
            cmd.Parameters.AddWithValue("@email", txtCadEmail.Text);
            
            reader = cmd.ExecuteReader();
            
            if (reader.HasRows && reader.Read())
            {
                //email já cadastrado
                txtCadEmail.BorderColor = System.Drawing.Color.Red;

                txtCadEmail.Text = "Email já cadastrado!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Email já cadastrado!')", true);
                reader.Close();
                cn.Close();
            }
            else 
            {
                reader.Close();
                cn.Close();
                cn = new MySqlConnection(connString);
                cn.Open();
                csql = "insert into usuario (us_nome,us_senha,us_email) " + " values( @nome , @senha , @email )";
                cmd = new MySqlCommand(csql, cn);
                cmd.Parameters.AddWithValue("@nome", txtCadNome.Text);
                cmd.Parameters.AddWithValue("@senha", txtCadSenha.Text);
                cmd.Parameters.AddWithValue("@email", txtCadEmail.Text);
                cmd.ExecuteScalar();
                cn.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Usuário cadastrado com sucesso!')", true);
            }
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = String.Format("{0}", Request.Form["loginemail"]);
            string senha = String.Format("{0}", Request.Form["loginpass"]);
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["tiawconnstring"].ToString();
            cn = new MySqlConnection(connString);
            cn.Open();
            csql = "SELECT * FROM usuario WHERE us_email = '" + email + "' and us_senha = '" + senha + "'";
            cmd = new MySqlCommand(csql, cn);
            reader = cmd.ExecuteReader();
            username = "";
            while (reader.HasRows && reader.Read())
            {
                username = reader.GetString(reader.GetOrdinal("us_lineardomain"));
                Session["ucod"] = int.Parse(reader.GetString(reader.GetOrdinal("us_cod")));

            }
            reader.Close();
            cn.Close();
        }
    }
}