# BackRomo — API REST

Backend del sistema **ROMO** para la gestión de reservas y despacho de grúas. Permite a clientes registrar solicitudes de servicio, a administradores asignar unidades y operadores, y llevar el control de operaciones, facturación y reportes.

---

## Arquitectura

El proyecto sigue una arquitectura limpia en cuatro capas desacopladas:

```
BackRomo.API           → Controladores, middlewares, configuración HTTP
BackRomo.Application   → Servicios de negocio, interfaces, DTOs
BackRomo.Domain        → Entidades y enumeraciones del dominio
BackRomo.Infrastructure → Repositorios (Dapper), JWT, Google Maps
```

Las dependencias fluyen hacia adentro: `API → Application → Domain`, con `Infrastructure` implementando las interfaces definidas en `Application`.

---

## Stack tecnológico

| Área            | Tecnología                              |
|-----------------|-----------------------------------------|
| Runtime         | .NET 10 / ASP.NET Core                  |
| Acceso a datos  | Dapper (SQL raw + stored procedures)    |
| Base de datos   | PostgreSQL / SQL Server (switcheable)   |
| Autenticación   | JWT Bearer (claims-based)               |
| Logging         | Serilog → Console + Azure Blob Storage  |
| Contenedor      | Docker (puerto 8080)                    |
| Servicios externos | Google Maps API                      |

---

## Requisitos previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- PostgreSQL ≥ 15 **o** SQL Server 2019+
- Google Maps API Key (para cálculo de rutas y distancias)
- *(Opcional)* Azure Storage Account para persistir logs en la nube

---

## Configuración local

El archivo `appsettings.Development.json` está excluido del repositorio (`.gitignore`). Créalo en `BackRomo.API/` con la siguiente estructura y completa los valores:

```json
{
  "DbProvider": "PostgreSQL",
  "ConnectionStrings": {
    "PostgreSQL": "Host=localhost;Port=5432;Database=romo_db;Username=...;Password=...",
    "SqlServer": ""
  },
  "Jwt": {
    "Key": "clave-secreta-minimo-32-caracteres",
    "Issuer": "BackRomo",
    "Audience": "BackRomo",
    "ExpiresInMinutes": 60
  },
  "GoogleMaps": {
    "ApiKey": "tu-api-key-de-google-maps"
  },
  "Cors": {
    "AllowedOrigins": "http://localhost:4200"
  },
  "Serilog": {
    "WriteTo": [
      { "Name": "Console" }
    ]
  }
}
```

Para usar SQL Server, cambia `DbProvider` a `"SqlServer"` y completa su cadena de conexión.

---

## Ejecutar el proyecto

### Modo desarrollo

```bash
dotnet run --project BackRomo.API
```

La API quedará disponible en `http://localhost:5000`. Swagger UI accesible en `http://localhost:5000/swagger`.

### Con Docker

```bash
# Construir imagen
docker build -t back_romo .

# Ejecutar contenedor
docker run -p 8080:8080 \
  -e DbProvider=PostgreSQL \
  -e ConnectionStrings__PostgreSQL="Host=...;Database=romo_db;..." \
  -e Jwt__Key="clave-secreta" \
  -e GoogleMaps__ApiKey="tu-api-key" \
  back_romo
```

---

## Endpoints disponibles

Swagger documenta todos los endpoints en `GET /swagger`. Los recursos expuestos son:

| Recurso         | Prefijo                  | Descripción                                      |
|-----------------|--------------------------|--------------------------------------------------|
| Autenticación   | `/api/Auth`              | Login y emisión de tokens JWT                    |
| Clientes        | `/api/Clientes`          | CRUD y gestión de estado de clientes             |
| Operadores      | `/api/Operadores`        | CRUD, disponibilidad y próximos servicios        |
| Flota           | `/api/Flota`             | Gestión de grúas, mantenimiento y baja de unidades |
| Reservas        | `/api/Reservas`          | Creación y seguimiento de reservas por el cliente |
| Operaciones     | `/api/Operaciones`       | Asignación, inicio, finalización y cancelación  |
| Agenda          | `/api/Agenda`            | Horarios regulares y excepciones de disponibilidad |
| Configuración   | `/api/Configuracion`     | Parámetros del sistema y tarifarios              |
| Reportes        | `/api/Reportes`          | Listado de servicios con KPIs para cobranza      |
| Usuarios        | `/api/Usuarios`          | CRUD de usuarios del sistema                     |

Todos los endpoints (excepto `/api/Auth/login`) requieren token JWT en el header `Authorization: Bearer <token>`.

---

## Rate limiting

El servidor aplica límites por ventana de tiempo para proteger los endpoints:

| Tipo de operación | Límite       |
|-------------------|--------------|
| Login             | 5 req/min    |
| Lectura (GET)     | 100 req/min  |
| Escritura (POST/PUT/PATCH/DELETE) | 30 req/min |

---

## Estructura de carpetas

```
back_romo/
├── BackRomo.API/
│   ├── Controllers/        # 10 controladores REST
│   ├── Middlewares/        # Manejo global de errores
│   ├── Program.cs          # Composición de servicios y pipeline HTTP
│   ├── appsettings.json    # Configuración base (sin secretos)
│   └── Dockerfile          # Build multi-stage .NET 10
│
├── BackRomo.Application/
│   ├── Services/           # 11 servicios de negocio
│   ├── Interfaces/         # Contratos de repositorios y servicios
│   └── DTOs/               # +70 DTOs organizados por dominio
│
├── BackRomo.Domain/
│   ├── Entities/           # 10 entidades del dominio
│   └── Enums/              # Estados de reserva, servicio, unidad y rol
│
├── BackRomo.Infrastructure/
│   ├── Repositories/       # 10 repositorios con Dapper
│   ├── Data/               # Fábrica de conexiones y type handlers
│   ├── Auth/               # Implementación de JwtService
│   └── Services/           # GoogleMapsService
│
└── Scripts/                # Scripts SQL auxiliares (funciones, SPs, índices)
```
