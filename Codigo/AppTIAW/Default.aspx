<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppTIAW.index" %>
<!DOCTYPE html>
<html lang="en">
    <head>
        <link rel="stylesheet" href="newCss.css">
        <title>Login</title>
    </head>
    <body>
        <h2>Bem-Vindo ao ProcrastinaApp faça seu login para entrar</h2>
        <div class="container" id="container">
            <div class="form-container sign-in-container">
                <form action="#" runat="server">
                    <h1>Login</h1>
                    <div class="social-container">
                        <a href="#" class="social"><i class="fab fa-facebook-f"></i></a>
                        <a href="#" class="social"><i class="fab fa-google-plus-g"></i></a>
                        <a href="#" class="social"><i class="fab fa-linkedin-in"></i></a>
                    </div>
                    <span>ou use sua conta</span>
                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Email"></asp:TextBox>
                    <asp:TextBox ID="txtSenha" runat="server" placeholder="Senha" TextMode="Password"></asp:TextBox>
                    <a href="#">Esqueceu sua senha?</a>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"/>
                        <asp:Label ID="lblErro" runat="server" Text="" ForeColor="Red" Font-Bold="True"></asp:Label>
                </form>
            </div>
            <div class="overlay-container">
                <div class="overlay">
                    <div class="overlay-panel overlay-right">
                        <h1>Olá Amigo!</h1>
                        <p>Cadastre-se para deixar de ser um procrastinador agora mesmo!</p>
                        <a href="/Cadastrar">
                            <button class="ghost" id="signUp">Cadastre-se</button></a>
                    </div>
                </div>
            </div>
        </div>
        
        <footer>
         
        </footer>
        <!-- The core Firebase JS SDK is always required and must be listed first -->
        <script src="https://www.gstatic.com/firebasejs/8.6.8/firebase-app.js"></script>

        <!-- TODO: Add SDKs for Firebase products that you want to use
            https://firebase.google.com/docs/web/setup#available-libraries -->
        <script src="https://www.gstatic.com/firebasejs/8.6.8/firebase-analytics.js"></script>

        <script>
            // Your web app's Firebase configuration
            // For Firebase JS SDK v7.20.0 and later, measurementId is optional
            var firebaseConfig = {
                apiKey: "AIzaSyDsVW-NbU7RH7UMOwt49GSoFmLpQwJ8ZWY",
                authDomain: "procrastinaapp.firebaseapp.com",
                projectId: "procrastinaapp",
                storageBucket: "procrastinaapp.appspot.com",
                messagingSenderId: "332464810730",
                appId: "1:332464810730:web:c215e0a77803eab38b4593",
                measurementId: "G-MCB0P9QJR8"
            };
            // Initialize Firebase
            firebase.initializeApp(firebaseConfig);
            firebase.analytics();
        </script>
    </body>
</html>
<script src="newMain.js"></script>