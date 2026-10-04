<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Denuncias.aspx.cs" Inherits="TP4Denuncias.Denuncias" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ABM Denuncias</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>ABM de Denuncias</h2>

            <asp:HiddenField ID="hfId" runat="server" />

            Categoría:
            <asp:DropDownList ID="ddlCategoria" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Value="">-- Seleccionar --</asp:ListItem>
            </asp:DropDownList>
            <br /><br />

            Denunciante:
            <asp:DropDownList ID="ddlDenunciante" runat="server" AppendDataBoundItems="true">
                <asp:ListItem Value="">-- Seleccionar --</asp:ListItem>
            </asp:DropDownList>
            <br /><br />

            Texto de la denuncia:<br />
            <asp:TextBox ID="txtTexto" runat="server" TextMode="MultiLine" Rows="4" Columns="50"></asp:TextBox>
            <br /><br />

            <asp:Button ID="btnAlta" runat="server" Text="Alta" OnClick="btnAlta_Click" />
            <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" />
            <asp:Button ID="btnBaja" runat="server" Text="Baja" OnClick="btnBaja_Click"
                OnClientClick="return confirm('¿Eliminar la denuncia seleccionada?');" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
            <br /><br />

            <asp:Label ID="lblMensaje" runat="server"></asp:Label>
            <br /><br />

            <asp:GridView ID="gv" runat="server" DataKeyNames="id"
                AutoGenerateSelectButton="True" CellPadding="4"
                OnSelectedIndexChanged="gv_SelectedIndexChanged">
                <SelectedRowStyle BackColor="Yellow" />
            </asp:GridView>
            <br />

            <a href="Default.aspx">Volver al menú</a>
        </div>
    </form>
</body>
</html>
