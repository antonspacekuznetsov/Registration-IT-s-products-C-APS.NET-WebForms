<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TANManager.aspx.cs" Inherits="RegITProducts.administator.pages.TANManager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="ButtonGenerateTAN" runat="server" Text="Сгенерировать ТАН" OnClick="ButtonGenerateTAN_Click" />
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="EntityDataSource1" DataTextField="CategoryName" DataValueField="Id">
        </asp:DropDownList>
        <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=ItProductsEntities" DefaultContainerName="ItProductsEntities" EnableFlattening="False" EntitySetName="Categories" Select="it.[Id], it.[CategoryName]">
        </asp:EntityDataSource>

    
        <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource1" DataTextField="ProductName" DataValueField="Id">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=127.0.0.1;Initial Catalog=ItProducts;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [ProductName], [Id] FROM [Products] WHERE ([CategoryID] = @CategoryID)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" DefaultValue="20" Name="CategoryID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>

    
    </div>
        <br />
        <asp:Button ID="ButtonShowTANs" runat="server" Text="Показать список ТАНов" OnClick="ButtonShowTANs_Click" />
        <br />
        <br />
        <asp:Label ID="LabelStatus" runat="server" Text="" ForeColor="Red"></asp:Label>
        <br />
        <asp:Label ID="LabelListOfTANs" runat="server" Text=""></asp:Label>
    </form>
</body>
</html>
