using System;
using System.IO;
using System.Web;

public static class Log
{
    public static void Registrar(string operacion, string tabla, string detalle)
    {
        string ruta = HttpContext.Current.Server.MapPath("~/App_Data/log.txt");
        Directory.CreateDirectory(Path.GetDirectoryName(ruta));

        string linea = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                     + " | " + operacion + " | " + tabla + " | " + detalle;
        File.AppendAllText(ruta, linea + Environment.NewLine);
    }
}