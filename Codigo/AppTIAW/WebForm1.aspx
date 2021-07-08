<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="AppTIAW.WebForm1" Culture="pt-BR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3><center><asp:Label ID="Label5" runat="server" Text="CONTROLE DE PROCRASTINAÇÃO" Font-Bold="True" Font-Overline="False" Font-Underline="True"></asp:Label> </center><br/></h3>
    
    <div class="jumbotron">
        <h4> <center> <asp:Label ID="Label6" runat="server" Text="Cadastre suas tarefas e pare de procrastinar!"></asp:Label></center><br/></h4>
      
    <center><asp:Calendar ID="Calendar1" runat="server" Height="462px" Width="826px" OnSelectionChanged="CarregarData"></asp:Calendar></center><br/>
    
     </div>
   <center> <asp:Label ID="Tarefas" runat="server" Text="Tarefas"  Font-Bold="True"></asp:Label></center>
    <div class="jumbotron">
      <center>  <asp:Label ID="Label1" runat="server" Text="" Font-Bold="True"></asp:Label> </center>
        <center><asp:GridView ID="GridView1" runat="server"  CaptionAlign="Top" Caption="Tarefas" BorderStyle="Groove" Font-Bold="False" GridLines="Horizontal" CssClass="mGrid1" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="ProcrastinarTarefa" OnRowCancelingEdit="GridView1_RowCancelingEdit">
             <Columns>  
                 <asp:CommandField ShowEditButton="true" EditText="PROCRASTINAR" ItemStyle-ForeColor="Red" />  
                 <asp:CommandField ShowDeleteButton="true" EditText="CONCLUIR" DeleteText="CONCLUIR" />  
             </Columns>  

                        <HeaderStyle BackColor="#009900" ForeColor="Black" Height="30px" CssClass="gridheader"/>
                        <RowStyle BackColor="#FFD0AF" />
                        <AlternatingRowStyle BackColor="#BCF5A9" />
                </asp:GridView></center><br />
       <center> <asp:Button ID="btnNovaTarefa" runat="server" Text="Nova Tarefa" OnClick="NovaTarefa" Visible="false"/> </center> <br/>

       <center> <asp:Panel ID="Panel1" runat="server" Visible="false">
            <asp:Label ID="Label2" runat="server" Text="Informações da tarefa: " Font-Bold="True"></asp:Label><br/><br/>
           <asp:Label ID="Label3" runat="server" Text="Descrição: " Font-Bold="True" Width="92px"></asp:Label><asp:TextBox ID="txtTarefa" runat="server" placeholder="Nome da tarefa" Width="115px"></asp:TextBox><br/>
          <asp:Label ID="Label4" runat="server" Text="Hora: " Font-Bold="True" Width="92px"></asp:Label> <asp:TextBox ID="txthora" runat="server" placeholder="Hora da tarefa" TextMode="Time" Width="115px"></asp:TextBox>
              <br/><br/>  <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" OnClick="AdicionarTarefa"></asp:Button>
        </asp:Panel></center>
    </div>
</asp:Content>
