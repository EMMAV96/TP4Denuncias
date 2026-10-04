using System;
using MySql.Data.MySqlClient;

namespace TP4Denuncias
{
    public partial class Categorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            gv.DataSource = Datos.Consultar("SELECT id, nombre FROM DenunciaCategorias ORDER BY id");
            gv.DataBind();
        }

        private void Limpiar()
        {
            hfId.Value = "";
            txtNombre.Text = "";
            gv.SelectedIndex = -1;
        }

        protected void gv_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gv.SelectedDataKey.Value.ToString();
            hfId.Value = id;
            var dt = Datos.Consultar("SELECT nombre FROM DenunciaCategorias WHERE id = @id",
                new MySqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                txtNombre.Text = dt.Rows[0]["nombre"].ToString();
            }
            lblMensaje.Text = "";
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            if (nombre == "")
            {
                lblMensaje.Text = "Ingresá el nombre de la categoría.";
                return;
            }

            Datos.Ejecutar("INSERT INTO DenunciaCategorias (nombre) VALUES (@nombre)",
                new MySqlParameter("@nombre", nombre));
            Log.Registrar("ALTA", "DenunciaCategorias", nombre);

            lblMensaje.Text = "Se registró la categoría.";
            Limpiar();
            CargarGrilla();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            if (hfId.Value == "")
            {
                lblMensaje.Text = "Seleccioná primero una categoría de la grilla.";
                return;
            }
            if (nombre == "")
            {
                lblMensaje.Text = "Ingresá el nombre de la categoría.";
                return;
            }

            Datos.Ejecutar("UPDATE DenunciaCategorias SET nombre = @nombre WHERE id = @id",
                new MySqlParameter("@nombre", nombre),
                new MySqlParameter("@id", hfId.Value));
            Log.Registrar("MODIFICACION", "DenunciaCategorias", "id=" + hfId.Value + " nombre=" + nombre);

            lblMensaje.Text = "Se modificó la categoría.";
            Limpiar();
            CargarGrilla();
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            if (hfId.Value == "")
            {
                lblMensaje.Text = "Seleccioná primero una categoría de la grilla.";
                return;
            }

            try
            {
                Datos.Ejecutar("DELETE FROM DenunciaCategorias WHERE id = @id",
                    new MySqlParameter("@id", hfId.Value));
                Log.Registrar("BAJA", "DenunciaCategorias", "id=" + hfId.Value);

                lblMensaje.Text = "Se eliminó la categoría.";
                Limpiar();
                CargarGrilla();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1451)
                    lblMensaje.Text = "No se puede eliminar: hay denuncias que usan esta categoría.";
                else
                    throw;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
            lblMensaje.Text = "";
        }
    }
}