﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonInput.aspx.cs" Inherits="WebClient.UserForms.PersonInput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../CSS/PopupPromp.css">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            Введите данные для создания Person 
        </h1>
    <p> ИНН : <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
               ControlToValidate="TextBox1" runat="server" ErrorMessage="Only Numbers allowed"
                ValidationExpression="\d+"></asp:RegularExpressionValidator>   
    </p>
    <p> Имя : <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
       
    </p>
    <p> Фамилия : <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
       
    </p>
         <asp:Button ID="Button1"  runat="server" class="opener" Text="Создать Person" OnClick="CreatePerson" Width="118px"/> 
        </div>
        <div id="captcha">
                Введите текст с картинки
            <br/>
            <img src="/Classes/CaptchaHandler.ashx" />
            <asp:TextBox ID="CaptchaValue" runat="server"></asp:TextBox>
        </div>
        <script type="text/javascript" src="../Scripts/PopupPromp.js"></script>
    </form>
</body>
</html>
