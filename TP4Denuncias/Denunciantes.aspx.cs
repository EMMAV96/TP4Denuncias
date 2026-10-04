using System;
using MySql.Data.MySqlClient;

namespace TP4Denuncias
{
    public partial class Denunciantes : System.Web.UI.Page
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
            gv.DataSource = Datos.Consultar("SELECT id, apellido, nombre FROM Denunciantes ORDER BY id");
            gv.DataBind();
        }

        private void Limpiar()
        {
            hfId.Value = "";
            txtApellido.Text = "";
            txtNombre.Text = "";
            gv.SelectedIndex = -1;
        }

        protected void gv_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = gv.SelectedDataKey.Value.ToString();
            hfId.Value = id;
            var dt = Datos.Consultar("SELECT apellido, nombre FROM Denunciantes WHERE id = @id",
                new MySqlParameter("@id", id));
            if (dt.Rows.Count > 0)
            {
                txtApellido.Text = dt.Rows[0]["apellido"].ToString();
                txtNombre.Text = dt.Rows[0]["nombre"].ToString();
            }
            lblMensaje.Text = "";
        }

        protected void btnAlta_Click(object sender, EventArgs e)
        {
            string apellido = txtApellido.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            if (apellido == "" || nombre == "")
            {
                lblMensaje.Text = "Ingresá apellido y nombre.";
                return;
            }

            Datos.Ejecutar("INSERT INTO Denunciantes (apellido, nombre) VALUES (@apellido, @nombre)",
                new MySqlParameter("@apellido", apellido),
                new MySqlParameter("@nombre", nombre));
            Log.Registrar("ALTA", "Denunciantes", apellido + ", " + nombre);

            lblMensaje.Text = "Se registró el denunciante.";
            Limpiar();
            CargarGrilla();
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string apellido = txtApellido.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            if (hfId.Value == "")
            {
                lblMensaje.Text = "Seleccioná primero un denunciante de la grilla.";
                return;
            }
            if (apellido == "" || nombre == "")
            {
                lblMensaje.Text = "Ingresá apellido y nombre.";
                return;
            }

            Datos.Ejecutar("UPDATE Denunciantes SET apellido = @apellido, nombre = @nombre WHERE id = @id",
                new MySqlParameter("@apellido", apellido),
                new MySqlParameter("@nombre", nombre),
                new MySqlParameter("@id", hfId.Value));
            Log.Registrar("MODIFICACION", "Denunciantes", "id=" + hfId.Value + " " + apellido + ", " + nombre);

            lblMensaje.Text = "Se modificó el denunciante.";
            Limpiar();
            CargarGrilla();
        }

        protected void btnBaja_Click(object sender, EventArgs e)
        {
            if (hfId.Value == "")
            {
                lblMensaje.Text = "Seleccioná primero un denunciante de la grilla.";
                return;
            }

            try
            {
                Datos.Ejecutar("DELETE FROM Denunciantes WHERE id = @id",
                    new MySqlParameter("@id", hfId.Value));
                Log.Registrar("BAJA", "Denunciantes", "id=" + hfId.Value);

                lblMensaje.Text = "Se eliminó el denunciante.";
                Limpiar();
                CargarGrilla();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1451)
                    lblMensaje.Text = "No se puede eliminar: el denunciante tiene denuncias registradas.";
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