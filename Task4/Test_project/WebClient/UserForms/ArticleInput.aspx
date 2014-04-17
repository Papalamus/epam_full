<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleInput.aspx.cs" Inherits="WebClient.UserForms.ArticleInput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            Введите данные для создания Article 
        </h1>
    <p> Код статьи : <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
               ControlToValidate="TextBox1" runat="server" ErrorMessage="Only Numbers allowed"
                ValidationExpression="\d+"></asp:RegularExpressionValidator>   
    </p>
    <p> Стоимость : <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
               ControlToValidate="TextBox2" runat="server" ErrorMessage="Only Numbers allowed"
                ValidationExpression="\d+"></asp:RegularExpressionValidator>   
       
    </p>
    <p> Название : <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
       
    </p>
         <asp:Button ID="Button1"  runat="server" Text="Создать Article" OnClick="CreatePerson" Width="118px"/> 
        </div>
    </form>
</body>
</html>