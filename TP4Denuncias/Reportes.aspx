<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="TP4Denuncias.Reportes" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reportes</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Cantidad de denuncias por categoría</h2>
            <asp:GridView ID="gvCategorias" runat="server" CellPadding="4" Caption="Denuncias por categoría">
            </asp:GridView>
            <br />

            <h2>Cantidad de denuncias por denunciante</h2>
            <asp:GridView ID="gvDenunciantes" runat="server" CellPadding="4" Caption="Denuncias por denunciante">
            </asp:GridView>
            <br />

            <a href="Default.aspx">Volver al menú</a>
        </div>
    </form>
</body>
</html>