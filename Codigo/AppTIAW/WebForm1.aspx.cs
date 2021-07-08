using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using MySql.Data.MySqlClient;
namespace AppTIAW
{
    public partial class WebForm1 : System.Web.UI.Page
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
            var enUsCulture = new CultureInfo("pt-BR");
            if (Session["ucod"] == null)
            {
                Response.Redirect("/");
            }
            else 
            {
                user = Session["ucod"].ToString();
            }
        }

        protected void CarregarData(object sender, EventArgs e)
        {
            btnNovaTarefa.Visible = true;
            Panel1.Visible = false;
            dataSelecionada = Calendar1.SelectedDate;
            Label1.Text = "EVENTOS DO DIA " + dataSelecionada.ToString("dd/MM/yyyy");

            
            
          
            cn = new MySqlConnection(connString);
            cn.Open();
            csql = "SELECT CONVERT(DATE_FORMAT(tarefa_data,'%d/%m/%Y'), CHAR) as DATA, tarefa_hora as HORA, tarefa_descricao as DESCRICAO, s.tarefa_status_desc as STATUS FROM tarefas t inner join tarefas_status s on t.tarefa_status_cod = s.tarefa_status_cod where tarefa_data = @data and us_cod = @usuario order by tarefa_hora ";
            cmd = new MySqlCommand(csql, cn);
            
            cmd.Parameters.AddWithValue("@data", dataSelecionada.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@usuario", user);
            reader = cmd.ExecuteReader();
            GridView1.DataSource = reader;
            GridView1.DataBind();
       

            reader.Close();
            cn.Close();
        }

        protected void AdicionarTarefa(object sender, EventArgs e)
        {
            if (txtTarefa.Text != "" && txthora.Text != "")
            {
                cn = new MySqlConnection(connString);
                cn.Open();
                csql = "insert into tarefas (us_cod,tarefa_data,tarefa_hora,tarefa_descricao) values( @usuario , @data ,  @hora , @descricao )";
                cmd = new MySqlCommand(csql, cn);

                cmd.Parameters.AddWithValue("@data", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@usuario", user);
                cmd.Parameters.AddWithValue("@hora", txthora.Text);
                cmd.Parameters.AddWithValue("@descricao", txtTarefa.Text);
                cmd.Prepare();
                cmd.ExecuteScalar();
                cn.Close();

                CarregarData(this, e);
            }
        }

        protected void NovaTarefa(object sender, EventArgs e)
        {
            Panel1.Visible = true;
        }

        int countUpdate;
        protected void ProcrastinarTarefa(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            string hora = row.Cells[3].Text;
            string descricao = row.Cells[4].Text;

            cn = new MySqlConnection(connString);
            cn.Open();
            csql = "update tarefas set tarefa_status_cod = 2, tarefa_data = @dataseguinte where us_cod = @usuario and tarefa_data = @data and tarefa_hora = @hora and tarefa_descricao = @descricao ";
            cmd = new MySqlCommand(csql, cn);

            cmd.Parameters.AddWithValue("@data", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
            string dataseguinte = (Calendar1.SelectedDate.AddDays(1)).ToString("yyyy-MM-dd");
            cmd.Parameters.AddWithValue("@dataseguinte", dataseguinte);
            cmd.Parameters.AddWithValue("@usuario", user);
            cmd.Parameters.AddWithValue("@hora", hora);
            cmd.Parameters.AddWithValue("@descricao", descricao);
            cmd.Prepare();
            cmd.ExecuteScalar();
            cn.Close();
            dataseguinte = (Calendar1.SelectedDate.AddDays(1)).ToString("dd/MM/yyyy");
            CarregarData(this, e);
            countUpdate++;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Você procrastinou a tarefa "+descricao+" para o dia "+dataseguinte+" ')", true);
            if (descricao == "")
            {
                Response.Redirect("/Webform1");
            }
            
           
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)//CONCLUIR
        {
            GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];
            string hora = row.Cells[3].Text;
            string descricao = row.Cells[4].Text;

            cn = new MySqlConnection(connString);
            cn.Open();
            csql = "update tarefas set tarefa_status_cod = 3 where us_cod = @usuario and tarefa_data = @data and tarefa_hora = @hora and tarefa_descricao = @descricao ";
            cmd = new MySqlCommand(csql, cn);

            cmd.Parameters.AddWithValue("@data", Calendar1.SelectedDate.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@usuario", user);
            cmd.Parameters.AddWithValue("@hora", hora);
            cmd.Parameters.AddWithValue("@descricao", descricao);
            cmd.Prepare();
            cmd.ExecuteScalar();
            cn.Close();

            CarregarData(this, e);

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewUpdateEventArgs e2 = new GridViewUpdateEventArgs(e.NewEditIndex);
            ProcrastinarTarefa(this, e2);
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Response.Redirect("/Webform1");
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            Response.Redirect("/Webform1");
        }
    }
}