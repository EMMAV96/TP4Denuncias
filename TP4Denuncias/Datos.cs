using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

public static class Datos
{
    private static string Cadena
    {
        get { return ConfigurationManager.ConnectionStrings["ISSD"].ConnectionString; }
    }

    // SELECT: devuelve una tabla para mostrar en grillas o llenar listas
    public static DataTable Consultar(string sql, params MySqlParameter[] parametros)
    {
        using (MySqlConnection cn = new MySqlConnection(Cadena))
        using (MySqlCommand cmd = new MySqlCommand(sql, cn))
        {
            cmd.Parameters.AddRange(parametros);
            DataTable dt = new DataTable();
            new MySqlDataAdapter(cmd).Fill(dt);
            return dt;
        }
    }

    // INSERT / UPDATE / DELETE: devuelve filas afectadas
    public static int Ejecutar(string sql, params MySqlParameter[] parametros)
    {
        using (MySqlConnection cn = new MySqlConnection(Cadena))
        using (MySqlCommand cmd = new MySqlCommand(sql, cn))
        {
            cmd.Parameters.AddRange(parametros);
            cn.Open();
            return cmd.ExecuteNonQuery();
        }
    }
}