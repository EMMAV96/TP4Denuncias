using System;
using MySql.Data.MySqlClient;

namespace TP4Denuncias
{
    public partial class Denuncias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarListas();
                CargarGrilla();
            }
        }

        private void CargarListas()
        {
            ddlCategoria.DataSource = Datos.Consultar("SELECT id, nombre FROM DenunciaCategorias ORDER BY nombre");
            ddlCategoria.DataValueField = "id";
            ddlCategoria.DataTextField = "nombre";
            ddlCategoria.DataBind();

            ddlDenunciante.DataSource = Datos.Consultar(
                "SELECT id, CONCAT(apellido, ', ', nombre) AS nombreCompleto FROM Denunciantes ORDER BY apellido, nombre");
            ddlDenunciante.DataValueField = "id";
            ddlDenunciante.DataTextField = "nombreCompleto";
            ddlDenunciante.DataBind();
        }

        private void CargarGrilla()
        {
            gv.DataSource = Datos.Consultar(
                "SELECT d.id, d.texto, c.nombre AS categoria, CONCAT(p.apellido, ', ', p.nombre) AS denunciante " +
                "FROM Denuncias d " +
                "JOIN DenunciaCategorias c ON c.id = d.idDenunciaCategoria " +
                "JOIN Denunciantes p ON p.id = d.idDenunciante " +
                "ORDER BY d.id");
            gv.DataBind();
        }

        private void Limpiar()
        {
            hfId.Value = "";
            txtTexto.Text = "";
            ddlCategoria.SelectedIndex = 0;
            ddlDenunciante.SelectedIndex = 0;
            gv.SelectedIndex = -1;
        }

        private bool Validar()
        {
            if (ddlCategoria.SelectedValue == "")
            {
                lblMensaje.Text = "Seleccioná una categoría.";
                return false;
            }
            if (ddlDenunciante.SelectedValue == "")
            {
                lblMensaje.Text = "Seleccioná un denunciante.";
                return false;
            }
            if (txtTexto.Text.Trim() == "")
            {
                lblMensaje.Text = "Ingresá el texto de la denuncia.";
                return false;
            }
            return true;
        }

        private string TextoParaLog()
        {
            return txtTexto.Text.Trim().Replace("\r", " ").Replace("\n", " ");
        }

        protected void gv_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gv.SelectedDataKey.Value.ToString();
            hfId.Value = id;
            var dt = Datos.Consultar(
                "SELECT texto, idDenunciaCategoria, idDenunciante FROM Denuncias WHERE id = @id",
                new MySqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                txtTexto.Text = dt.Rows[0]["texto"].ToString();
                ddlCategoria.SelectedValue = dt.Rows[0]["idDenunciaCategoria"].ToString();
                ddlDenunciante.SelectedValue = dt.Rows[0]["idDenunciante"].ToString();
            }
            lblMensaje.Text = "";
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            if (!Validar()) return;

            Datos.Ejecutar(
                "INSERT INTO Denuncias (texto, idDenunciaCategoria, idDenunciante) VALUES (@texto, @cat, @den)",
                new MySqlParameter("@texto", txtTexto.Text.Trim()),
                new MySqlParameter("@cat", ddlCategoria.SelectedValue),
                new MySqlParameter("@den", ddlDenunciante.SelectedValue));
            Log.Registrar("ALTA", "Denuncias",
                "cat=" + ddlCategoria.SelectedValue + " den=" + ddlDenunciante.SelectedValue + " texto=" + TextoParaLog());

            lblMensaje.Text = "Se registró la denuncia.";
            Limpiar();
            CargarGrilla();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (hfId.Value == "")
            {
                lblMensaje.Text = "Seleccioná primero una denuncia de la grilla.";
                return;
            }
            if (!Validar()) return;

            Datos.Ejecutar(
                "UPDATE Denuncias SET texto = @texto, idDenunciaCategoria = @cat, idDenunciante = @den WHERE id = @id",
                new MySqlParameter("@texto", txtTexto.Text.Trim()),
                new MySqlParameter("@cat", ddlCategoria.SelectedValue),
                new MySqlParameter("@den", ddlDenunciante.SelectedValue),
                new MySqlParameter("@id", hfId.Value));
            Log.Registrar("MODIFICACION", "Denuncias",
                "id=" + hfId.Value + " cat=" + ddlCategoria.SelectedValue + " den=" + ddlDenunciante.SelectedValue + " texto=" + TextoParaLog());

            lblMensaje.Text = "Se modificó la denuncia.";
            Limpiar();
            CargarGrilla();
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            if (hfId.Value == "")
            {
                lblMensaje.Text = "Seleccioná primero una denuncia de la grilla.";
                return;
            }

            Datos.Ejecutar("DELETE FROM Denuncias WHERE id = @id",
                new MySqlParameter("@id", hfId.Value));
            Log.Registrar("BAJA", "Denuncias", "id=" + hfId.Value);

            lblMensaje.Text = "Se eliminó la denuncia.";
            Limpiar();
            CargarGrilla();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            lblMensaje.Text = "";
        }
    }
}