using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;
namespace AppTIAW
{
    public partial class Perfil : System.Web.UI.Page
    {
        MySqlConnection cn;
        MySqlCommand cmd;
        String csql;
        MySqlDataReader reader;
        DateTime dataSelecionada;
        string connString;
        string user;
        protected void Page_Load(object sender, EventArgs e)
        {
            connString = System.Configuration.ConfigurationManager.ConnectionStrings["tiawconnstring"].ToString();
            if (Session["ucod"] == null)
            {
                Response.Redirect("/");
            }
            else
            {
                user = Session["ucod"].ToString();
                if (HiddenCountReload.Value == "0")
                {
                    CarregarPerfil();
                    HiddenCountReload.Value = "1";
                }
            }

        }

        protected void CarregarPerfil() 
        {
            cn = new MySqlConnection(connString);
            cn.Open();
            csql = "SELECT u.*, s.us_cod AS codsocial, us_facebook, us_instagram, us_github, us_twitter, us_website FROM usuario u  LEFT JOIN usuario_social s ON u.us_cod = s.us_cod where u.us_cod = @usuario ";
            cmd = new MySqlCommand(csql, cn);

           
            cmd.Parameters.AddWithValue("@usuario", user);
            reader = cmd.ExecuteReader();
            while (reader.Read()) 
            {
                lblNomeUsuario.Text = reader["us_nome"].ToString();
                lblEnderecoUsuario.Text = reader["us_endereco"].ToString();
                txtEndereco.Text = reader["us_endereco"].ToString();
                txtNome.Text = reader["us_nome"].ToString();
                txtTelefone.Text = reader["us_telefone"].ToString();
                txtEmail.Text = reader["us_email"].ToString();
                txtCelular.Text = reader["us_celular"].ToString();
                if (reader["codsocial"] != null) 
                {
                    lblWebsite.Text = reader["us_website"].ToString();
                    txtWebsite.Text = reader["us_website"].ToString();
                    lblGitHub.Text = reader["us_github"].ToString();
                    txtGitHub.Text = reader["us_github"].ToString();
                    
                    txtFacebook.Text = reader["us_facebook"].ToString();
                    lblFacebook.Text = reader["us_facebook"].ToString();
                    txtInstagram.Text = reader["us_instagram"].ToString();
                    lblInstagram.Text = reader["us_instagram"].ToString();
                    
                    
                }
                
            }


            reader.Close();
            cn.Close();

            CarregarStatusTerefas();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            user = Session["ucod"].ToString();
            cn = new MySqlConnection(connString);
            cn.Open();
            csql = "UPDATE usuario set us_nome = @nome , us_email = @email, us_endereco = @endereco, us_telefone = @telefone, us_celular = @celular where us_cod = @usuario ";
            cmd = new MySqlCommand(csql, cn);

            cmd.Parameters.AddWithValue("@nome", txtNome.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@endereco", txtEndereco.Text);
            cmd.Parameters.AddWithValue("@telefone", txtTelefone.Text);
            cmd.Parameters.AddWithValue("@celular", txtCelular.Text);
            cmd.Parameters.AddWithValue("@usuario", user);
            cmd.ExecuteScalar();
            cn.Close();
            CarregarPerfil();
            
        }
        
            protected void CarregarStatusTerefas()
            {
            string TarefasPendentes = "";
            string TarefasConcluidas = "";
            string TarefasProcrastinadas = "";
            string TotalTarefas = "";
            user = Session["ucod"].ToString();
            cn = new MySqlConnection(connString);
            cn.Open();
            csql = "select count(*) as count from tarefas where us_cod = @usuario "; // TOTAL DE TAREFAS
            cmd = new MySqlCommand(csql, cn);
            cmd.Parameters.AddWithValue("@usuario", user);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
             TotalTarefas = reader["count"].ToString();
            }
            reader.Close();
            csql = "select count(*) as count from tarefas where us_cod = @usuario and tarefa_status_cod = 1 "; // TAREFAS PENDENTES
            cmd = new MySqlCommand(csql, cn);
            cmd.Parameters.AddWithValue("@usuario", user);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TarefasPendentes = reader["count"].ToString();
            }
            reader.Close();
            csql = "select count(*) as count from tarefas where us_cod = @usuario and tarefa_status_cod = 2 "; // TAREFAS PROCRASTINADAS
            cmd = new MySqlCommand(csql, cn);
            cmd.Parameters.AddWithValue("@usuario", user);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TarefasProcrastinadas = reader["count"].ToString();
            }
            reader.Close();
            csql = "select count(*) as count from tarefas where us_cod = @usuario and tarefa_status_cod = 3 "; // TAREFAS CONCLUIDAS
            cmd = new MySqlCommand(csql, cn);
            cmd.Parameters.AddWithValue("@usuario", user);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TarefasConcluidas = reader["count"].ToString();
            }


            reader.Close();
            cn.Close();
            decimal tempporcCconcluidas = (int.Parse(TarefasConcluidas) * 100) / int.Parse(TotalTarefas);
            decimal tempporcPendentes = (int.Parse(TarefasPendentes) * 100) / int.Parse(TotalTarefas);
            decimal tempporcProcrastinadas = (int.Parse(TarefasProcrastinadas) * 100) / int.Parse(TotalTarefas);
            int porcCconcluidas = int.Parse(Math.Round(tempporcCconcluidas).ToString());
            int porcPendentes = int.Parse(Math.Round(tempporcPendentes).ToString());
            int porcProcrastinadas = int.Parse(Math.Round(tempporcProcrastinadas).ToString());

            lblTarefasConcluidas.Text = TarefasConcluidas+"/"+TotalTarefas + " - " + porcCconcluidas + "%";
            lblTarefasPendentes.Text = TarefasPendentes + "/" + TotalTarefas + " - " + porcPendentes + "%";
            lblTarefasProcrastinadas.Text = TarefasProcrastinadas + "/" + TotalTarefas + " - " + porcProcrastinadas + "%";

            if (porcProcrastinadas > 40)
            {
                lblStatus.Text = "É Procrastinador";
            }
            else 
            {
                if (porcProcrastinadas == 0)
                {
                    lblStatus.Text = "Não é nada procrastinador";
                }
                lblStatus.Text = " Não é um procrastinador";
            }

            //HtmlGenericControl procrastinadas = new HtmlGenericControl();
            //procrastinadas = (HtmlGenericControl)Master.FindControl("procrastinadas");
            //procrastinadas.Attributes["style"] = "width: "+porcProcrastinadas+"%";
            //procrastinadas.Attributes["aria-valuenow"] = porcProcrastinadas.ToString();
            //procrastinadas.Attributes["aria-valuemin"] = "0";//FIXED
            //procrastinadas.Attributes["aria-valuemax"] = "100";//FIXED

            //HtmlGenericControl pendentes;
            //procrastinadas = (HtmlGenericControl)Master.FindControl("procrastinadas");
            //procrastinadas.Attributes["style"] = "width: 80%";
            //procrastinadas.Attributes["aria-valuenow"] = "80";
            //procrastinadas.Attributes["aria-valuemin"] = "0";//FIXED
            //procrastinadas.Attributes["aria-valuemax"] = "100";//FIXED



        }
    }
}