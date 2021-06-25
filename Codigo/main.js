function verificaLogin(){
    if(document.getElementsByClassName('txtLogin').value == $('LOGIN') && document.getElementsByClassName('txtSenha').value == $('SENHA')){
        window.location.href = "cronometro.html";
    }
}

function cadastrar() {
    let senha;
    let senha2;
    senha = document.querySelector("#senha").value;
    senha2 = document.querySelector("#senha2").value;
    if (senha !== senha2) {
        window.alert('As senhas não conferem!');
    }
}