# TP4 - Denuncias (Sistemas de Información IV)

Mini CRUD en ASP.NET Web Forms (C#, .NET Framework 4.8) con MySQL.

## Qué incluye
- ABM de DenunciaCategorias, Denunciantes y Denuncias.
- Log de operaciones ABM con fecha en `TP4Denuncias/App_Data/log.txt` (se crea solo al realizar la primera operación).
- Reportes: cantidad de denuncias por categoría y por denunciante.

## Requisitos
- Visual Studio con la carga de trabajo "Desarrollo de ASP.NET y web" y .NET Framework 4.8.
- XAMPP con MySQL iniciado (puerto 3306).
- Conexión a internet la primera vez (restaura el paquete NuGet `MySql.Data`).

## Cómo probarlo
1. Iniciar MySQL en XAMPP.
2. Ejecutar `script_bd.sql` en MySQL Workbench (o phpMyAdmin). Crea la base `ISSD-TP4-202601`, las tablas y datos de prueba.
3. Abrir `TP4Denuncias.sln` en Visual Studio.
4. Revisar la cadena de conexión en `TP4Denuncias/Web.config` (por defecto usuario `root` sin contraseña). Si tu MySQL tiene contraseña, completarla en `Pwd=`.
5. Ejecutar con F5. La página de inicio es el menú (`Default.aspx`).