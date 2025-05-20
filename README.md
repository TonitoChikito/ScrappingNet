# 📰 Proyecto de Scraping de Noticias en .NET
**Extrae, almacena y gestiona noticias de BBC News automáticamente.**

---

## 🚀 Empezar desde Cero

### 📦 Requisitos Mínimos
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB o instancia instalada)
- Navegador web (Chrome, Firefox, etc.)
- Postman (opcional para probar la API REST)

---

## 🗂️ Estructura del Proyecto

```plaintext
PruebaTecnica/
├── Controllers/
│   └── HomeController.cs        # Controlador de vista principal
├── Data/
│   └── AppDbContext.cs          # Configuración de la base de datos
├── Extensions/
│   └── EndpointExtensions.cs    # Lógica de scraping y rutas API
├── Models/
│   └── Noticia.cs               # Modelo de datos
├── Views/
│   └── Home/Index.cshtml        # Vista principal
├── wwwroot/                     # Archivos estáticos
├── appsettings.json             # Configuración de conexión
├── Program.cs                   # Punto de entrada
└── PruebaTecnica.csproj         # Archivo de proyecto
```

---

## 🎯 Funcionalidades Clave

### 1. 🔍 Scraping Automático

- **Endpoint:** `/scrape`
- **Tecnología:** `HtmlAgilityPack` con XPath
- **Cómo usarlo:**
  1. Ejecuta `dotnet run`
  2. En tu navegador visita `http://localhost:{puerto}/scrape`
- **Resultado esperado:** `"Scraping completado y datos guardados en la base de datos."`

---

### 2. 🧩 API REST

| **Ruta**           | **Método** | **Función**                            | **Ejemplo cURL**                                                                 |
|--------------------|------------|----------------------------------------|----------------------------------------------------------------------------------|
| `/noticias`        | GET        | Lista todas las noticias               | `curl http://localhost:5000/noticias`                                            |
| `/noticia/{id}`    | GET        | Obtener una noticia por ID             | `curl http://localhost:5000/noticia/1`                                           |
| `/noticia/{id}`    | PUT        | Actualizar una noticia existente       | `curl -X PUT -H "Content-Type: application/json" -d "{\"titulo\":\"Nuevo título\", \"descripcion\":\"Nueva desc\"}" http://localhost:5000/noticia/1` |
| `/noticia/{id}`    | DELETE     | Eliminar una noticia                   | `curl -X DELETE http://localhost:5000/noticia/1`                                 |

---

### 3. 🖥️ Interfaz Web

- **Ruta principal:** `/`
- **Función:** Muestra todas las noticias en tabla HTML con Razor (.cshtml)

---

## ⚙️ Configuración de la Base de Datos
> Este proyecto está configurado para conectarse a una instancia local de **SQL Server**, y puedes administrar visualmente la base de datos usando **SQL Server Management Studio (SSMS)**.

### Paso 1: Configurar la cadena de conexión en `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PruebaDb;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```
"PruebaDb será el nombre de la base de datos creada"
---

### Paso 2: Crear la base de datos automáticamente

```bash
dotnet ef migrations add InicialCreate
dotnet ef database update
```

Esto:
- Crea la base de datos `PruebaDb` (si no existe)
- Crea automáticamente la tabla `Noticias` basada en tu modelo

---

## 📦 Restaurar Paquetes NuGet

Ejecuta en terminal:

```bash
dotnet restore
```

| Paquete NuGet                         | Versión   | Descripción                                                   |
|--------------------------------------|-----------|---------------------------------------------------------------|
| HtmlAgilityPack                      | 1.11.54   | Analiza y extrae HTML desde la web (scraping)                 |
| Microsoft.EntityFrameworkCore        | 6.0.25    | ORM para manejar la base de datos                            |
| Microsoft.EntityFrameworkCore.SqlServer | 6.0.25 | Proveedor EF Core para SQL Server                             |
| Microsoft.EntityFrameworkCore.Tools  | 6.0.25    | Herramientas CLI para migraciones y gestión de DB             |
| Swashbuckle.AspNetCore               | 6.5.0     | Genera documentación Swagger para probar API desde el navegador |

---

## 🚀 Ejecución

```bash
dotnet run
```

> La consola indicará la URL, como: `http://localhost:5000`

---

---

## 🖼️ Evidencias de Funcionamiento

Todas las capturas de pantalla de pruebas y resultados (incluyendo el uso de Postman para las rutas `/scrape`, `/noticias`, `/noticia/{id}`, etc.) están almacenadas en la carpeta:

```plaintext
/ImagenesFuncionamiento/
```

> Esta carpeta contiene evidencia visual del scraping, inserción en base de datos y funcionamiento completo de la API REST.
