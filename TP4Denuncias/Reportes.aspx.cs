using System;

namespace TP4Denuncias
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvCategorias.DataSource = Datos.Consultar(
                    "SELECT c.nombre AS Categoria, COUNT(d.id) AS Cantidad " +
                    "FROM DenunciaCategorias c " +
                    "LEFT JOIN Denuncias d ON d.idDenunciaCategoria = c.id " +
                    "GROUP BY c.id, c.nombre " +
                    "ORDER BY c.nombre");
                gvCategorias.DataBind();

                gvDenunciantes.DataSource = Datos.Consultar(
                    "SELECT CONCAT(p.apellido, ', ', p.nombre) AS Denunciante, COUNT(d.id) AS Cantidad " +
                    "FROM Denunciantes p " +
                    "LEFT JOIN Denuncias d ON d.idDenunciante = p.id " +
                    "GROUP BY p.id, p.apellido, p.nombre " +
                    "ORDER BY p.apellido, p.nombre");
                gvDenunciantes.DataBind();
            }
        }
    }
}