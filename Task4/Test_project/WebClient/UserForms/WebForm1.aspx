<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebClient.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Форма доступа к БД</title>
    <link rel="stylesheet" type="text/css" href="StyleSheet.css">
</head>
<body>
    
    <div class="navbar-header">
        
        
       <ul id="nav">
            <li>
                <a href="#" title="">Выбрать сущность </a>
                <ul>
                    <li><a id="A1" href="#" name ="Person" runat="server" onServerClick="ChangeEntity"> Person</a></li>
                    <li><a id="A2" href="#" name ="Article"  runat="server" onServerClick="ChangeEntity"> Article</a></li>
                </ul>
            </li>
            <li>
                <a href="#" title="">Выбрать базу для хранения</a>
                <ul>
                    <li><a id="A3" href="#" name ="Ado" runat="server" onServerClick="ChangeConnecter"> Ado</a></li>
                    <li><a id="A4" href="#" name ="MyORM" runat="server" onServerClick="ChangeConnecter"> MyORM</a></li>
                    
                </ul>
            </li>
        </ul>
    </div>

    <form id="form1" runat="server">
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <p>
        <asp:Button  runat="server" Text="GetAll" OnClick="GetAll" Width="76px"/> 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <button>GetByID</button>
        &nbsp;&nbsp;&nbsp;
       
        <asp:Button ID="Button3"  runat="server" Text="Insert" OnClick="RedirectOnInput" Width="76px"/> 
        &nbsp;&nbsp;&nbsp;&nbsp;
        <button style="width: 61px">Delete</button>
        </p>
    <div>
       <p>Id объекта 
           <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
               ControlToValidate="TextBox1" runat="server" ErrorMessage="Only Numbers allowed"
                ValidationExpression="\d+"></asp:RegularExpressionValidator>   
           </p>
    </div>
    </form>
</body>
</html>
