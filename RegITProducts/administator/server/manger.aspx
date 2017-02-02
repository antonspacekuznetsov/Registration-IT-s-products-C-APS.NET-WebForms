<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manger.aspx.cs" Inherits="RegITProducts.administator.pages.ItemManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:DropDownList ID="DropDownListMain" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" Width="177px" AutoPostBack="True">
            <asp:ListItem Value="0">Категории</asp:ListItem>
            <asp:ListItem Value="1">Продукты</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="ButtonGetDataList" runat="server" Text="Загрузить/обновить список" OnClick="ButtonGetCategory_Click" />
        <asp:Label ID="LabelFromCategory" runat="server" Text="из категории"></asp:Label>
        <asp:DropDownList ID="DropDownListCategory" runat="server" DataSourceID="EntityDataSource1" DataTextField="CategoryName" DataValueField="Id">
        </asp:DropDownList>
        <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=ItProductsEntities" DefaultContainerName="ItProductsEntities" EnableFlattening="False" EntitySetName="Categories" Select="it.[CategoryName], it.[Id]">
        </asp:EntityDataSource>
        <br />
        <br />
        <asp:TextBox ID="TextBoxAddCategory" runat="server" Width="177px"></asp:TextBox>
        <asp:Button ID="ButtonAddCategory" runat="server" Text="Добавить" OnClick="ButtonAddCategory_Click" />
        <br />
        <br />
        <asp:TextBox ID="TextBoxDeleteCategory" runat="server" Width="177px" OnTextChanged="TextBoxDeleteCategory_TextChanged"></asp:TextBox>
        <asp:Button ID="ButtonDeleteCategory" runat="server" Text="Удалить" OnClick="ButtonDeleteCategory_Click" />
        <br />
        <br />
        <asp:TextBox ID="TextBoxEditInboxVal" runat="server" Width="177px"></asp:TextBox>
        <asp:TextBox ID="TextBoxEditOutboxVal" runat="server" Width="177px"></asp:TextBox>
        <asp:Button ID="ButtonEdit" runat="server" Text="Отредактировать" OnClick="ButtonEdit_Click" />
        <br />
        <asp:Label ID="LabelInbox" runat="server" Text="запись которую следует отредактировать" Font-Italic="True" Font-Size="Small" Width="177px"></asp:Label>
        <asp:Label ID="LabelOutbox" runat="server" Text="запись на которую следует заменить" Font-Italic="True" Font-Size="Small" Width="177px"></asp:Label>
        <br />
        <p>
            <asp:Label ID="LabelStatus" runat="server" ForeColor="Red"></asp:Label>
        </p>
        <p>
    
            <asp:Label ID="LabelInfo" runat="server" Text=""></asp:Label>
    
        </p>
        <p>
            &nbsp;</p>
        <asp:ListView ID="ListView1" runat="server">
        </asp:ListView>
    </form>
</body>
</html>
