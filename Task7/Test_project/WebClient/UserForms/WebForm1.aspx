<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebClient.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Форма доступа к БД</title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap.js"></script>
    
    <link rel="stylesheet" type="text/css" href="../CSS/bootstrap.css">
    <link rel="stylesheet" type="text/css" href="../CSS/bootstrap-theme.css">
    <link rel="stylesheet" type="text/css" href="../CSS/PopupPromp.css">
    
</head>
<body>
    <div class = "mainDiv">
    <div class = "btn-group">
      <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
        Выбрать сущность <span class="caret"></span>
      </button>
      <ul class="dropdown-menu" role="menu">
        <li><a id="A1" href="#" name ="Person" runat="server" onServerClick="ChangeEntity"> Person</a></li>
        <li><a id="A2" href="#" name ="Article"  runat="server" onServerClick="ChangeEntity"> Article</a></li>
      </ul>
    </div>
    
    <div class="btn-group">
      <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown">
        Выбрать базу для хранения <span class="caret"></span>
      </button>
      <ul class="dropdown-menu" role="menu">
        <li><a id="A3" href="#" name ="Ado" runat="server" onServerClick="ChangeEntity"> Person</a></li>
        <li><a id="A4" href="#" name ="MyORM"  runat="server" onServerClick="ChangeEntity"> Article</a></li>
      </ul>
    </div>
    

    <form id="form1" runat="server">
        <asp:Table ID="Table1" runat="server" BorderWidth="1px"  GridLines="Both" BorderColor="black"  BorderStyle="Solid" 
                CellPadding="30" >
            </asp:Table>
        <p>
        <asp:Button  runat="server" class ="opener" Text="GetAll" OnClick="GetAll" Width="100px" Height="22px"/> 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <button>GetByID</button>
        &nbsp;&nbsp;&nbsp;
       
        <asp:Button ID="Button3"  class ="opener" runat="server" Text="Insert" OnClick="RedirectOnInput" Width="100px" Height="22px"/> 
        
        </p>
    <div>
       <p>Id объекта 
           <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
           &nbsp;&nbsp;&nbsp;&nbsp;
             <button style="width: 61px"  class ="opener">Delete</button>
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
               ControlToValidate="TextBox1" runat="server" ErrorMessage="Only Numbers allowed"
                ValidationExpression="\d+"></asp:RegularExpressionValidator>   
               
           </p>
        <p>
            
        </p>
      
    </div>

        <div id="captcha">
                Введите текст с картинки
            <br/>
            <img src="/Classes/CaptchaHandler.ashx" />
            <asp:TextBox ID="CaptchaValue" runat="server"></asp:TextBox>
        </div>
        <script type="text/javascript" src="../Scripts/PopupPromp.js"></script>
    </form>
     </div>
    
    
</body>
</html>
