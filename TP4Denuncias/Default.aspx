<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TP4Denuncias.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sistema de Denuncias</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Sistema de Denuncias</h2>
            <p><asp:HyperLink ID="lnkCategorias" runat="server" NavigateUrl="~/Categorias.aspx">ABM de Categorías</asp:HyperLink></p>
            <p><asp:HyperLink ID="lnkDenunciantes" runat="server" NavigateUrl="~/Denunciantes.aspx">ABM de Denunciantes</asp:HyperLink></p>
            <p><asp:HyperLink ID="lnkDenuncias" runat="server" NavigateUrl="~/Denuncias.aspx">ABM de Denuncias</asp:HyperLink></p>
            <p><asp:HyperLink ID="lnkReportes" runat="server" NavigateUrl="~/Reportes.aspx">Reportes (denuncias por categoría y por denunciante)</asp:HyperLink></p>
        </div>
    </form>
</body>
</html>