<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RegITProducts.administator.user.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Введите ТАН
        <asp:TextBox ID="TextBoxTAN" runat="server"></asp:TextBox>
        <asp:Button ID="ButtonCheckTAN" runat="server" Text="Отправить" OnClick="ButtonCheckTAN_Click" />
    
    </div>
        <asp:Label ID="LabelInfo" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
