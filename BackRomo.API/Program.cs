using System.Text;
using System.Threading.RateLimiting;
using BackRomo.API.Middlewares;
using BackRomo.Application.Interfaces;
using BackRomo.Application.Services;
using BackRomo.Infrastructure.Auth;
using BackRomo.Infrastructure.Data;
using BackRomo.Infrastructure.Repositories;
using BackRomo.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, config) =>
    config.ReadFrom.Configuration(ctx.Configuration));

// Conexión a base de datos — cambiar "DbProvider" en appsettings.json: "SqlServer" | "PostgreSQL"
var dbProvider     = builder.Configuration["DbProvider"] ?? "SqlServer";
var connectionString = builder.Configuration.GetConnectionString(dbProvider)!;
builder.Services.AddSingleton(new DbConnectionFactory(connectionString, dbProvider));

// Auth
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddSingleton<IJwtService, JwtService>();

// Clientes
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ClienteService>();

// Configuracion
builder.Services.AddScoped<IConfiguracionRepository, ConfiguracionRepository>();
builder.Services.AddScoped<ConfiguracionService>();

// Agenda
builder.Services.AddScoped<IAgendaRepository, AgendaRepository>();
builder.Services.AddScoped<AgendaService>();

// Fecha local (zona horaria centralizada)
builder.Services.AddScoped<IFechaService, FechaService>();

// Reservas
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<ReservaService>();

// Operadores
builder.Services.AddScoped<IOperadorRepository, OperadorRepository>();
builder.Services.AddScoped<OperadorService>();

// Operaciones
builder.Services.AddScoped<IOperacionRepository, OperacionRepository>();
builder.Services.AddScoped<OperacionService>();

// Flota
builder.Services.AddScoped<IFlotaRepository, FlotaRepository>();
builder.Services.AddScoped<FlotaService>();

// Usuarios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

// Google Maps
builder.Services.AddHttpClient<IGoogleMapsService, GoogleMapsService>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT
var jwtConfig = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorizationBuilder()
    .SetFallbackPolicy(new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build());

// CORS (permite el front Angular en desarrollo)
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        var allowedOrigins = builder.Configuration["Cors:AllowedOrigins"] ?? string.Empty;

        if (allowedOrigins == "*")
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
        else
        {
            var origins = allowedOrigins.Split(';', StringSplitOptions.RemoveEmptyEntries);
            policy.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
        }
    });
});

// Request Timeouts
builder.Services.AddRequestTimeouts(options =>
{
    options.AddPolicy("corto", TimeSpan.FromSeconds(10));
    options.AddPolicy("largo",  TimeSpan.FromSeconds(30));
});

// Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddFixedWindowLimiter("login", config =>
    {
        config.PermitLimit          = 5;
        config.Window               = TimeSpan.FromMinutes(1);
        config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        config.QueueLimit           = 0;
    });

    options.AddFixedWindowLimiter("lectura", config =>
    {
        config.PermitLimit          = 100;
        config.Window               = TimeSpan.FromMinutes(1);
        config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        config.QueueLimit           = 0;
    });

    options.AddFixedWindowLimiter("escritura", config =>
    {
        config.PermitLimit          = 30;
        config.Window               = TimeSpan.FromMinutes(1);
        config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        config.QueueLimit           = 0;
    });
});

//Configuración zona horaria Npgsql
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseCors("FrontendPolicy");

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestTimeouts();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
