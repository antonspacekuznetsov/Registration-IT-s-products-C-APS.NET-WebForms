<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="RegITProducts.administator.user.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <h1>
    
        <asp:Label ID="LabelInfoProduct" runat="server" Text=""></asp:Label>
        </h1>
        <h1>Регистрация</h1>
        <p>Имя* <asp:TextBox ID="TextBoxName" runat="server"></asp:TextBox>
        </p>
        <p>Фамилия* <asp:TextBox ID="TextBoxSurname" runat="server"></asp:TextBox>
        </p>
        <p>Дата рождения*
            <asp:TextBox ID="TextBoxBirthDay" runat="server" TextMode="Date"></asp:TextBox>
        </p>
        <p>Адрес*&nbsp;
            <asp:TextBox ID="TextBoxAdress" runat="server"></asp:TextBox>
        </p>
        <p>Информация о продукте*
        </p>
    </div>
        <p>
            <asp:TextBox ID="TextBoxInfoAboutProduct" runat="server" Height="69px" Width="394px" TextMode="MultiLine"></asp:TextBox>
        </p>
        <p>
            Дата покупки*
            <asp:TextBox ID="TextBoxBuyDate" runat="server" TextMode="Date"></asp:TextBox>
        </p>
        <p>
            Дата регистрации
            <asp:TextBox ID="TextBoxRegisterDate" runat="server" ReadOnly="True" TextMode="Date"></asp:TextBox>
        </p>
        <p>
            Поля помеченные * - обязательны к заполнению</p>
        <p>
            <asp:Label ID="LabelError" runat="server" ForeColor="Red"></asp:Label>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" Text="Зарегестрироваться" OnClick="Button1_Click" />
        </p>
    </form>
</body>
</html>
