using System.Security.Cryptography;
using System.Text;
using BackRomo.Application.Interfaces;
using BackRomo.Domain.Entities;
using BackRomo.Infrastructure.Data;
using Dapper;

namespace BackRomo.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly DbConnectionFactory _db;

    public AuthRepository(DbConnectionFactory db)
    {
        _db = db;
    }
   /*  //Con sql server
    public async Task<Usuario?> LoginAsync(string identificador, string contrasena)
    {
        var hashContrasena = HashMd5ComoGuid(contrasena);

        using var conn = _db.CreateConnection();
        var result = await conn.QueryFirstOrDefaultAsync<SpLoginResult>(
         "sp_LoginUsuario",
         new { Identificador = identificador, Contrasena = hashContrasena },
         commandType: System.Data.CommandType.StoredProcedure
        );

        if (result is null || result.Exitoso == 0)
            return null;

        return new Usuario
        {
            Id         = result.Id,
            Alias      = result.Alias,
            Nombres    = result.Nombres,
            Apellidos  = result.Apellidos,
            Correo     = result.Correo,
            Telefono   = result.Telefono,
            Rol        = result.Rol,
            IdCliente  = result.IdCliente,
            TarifaKm   = result.TarifaKmCliente,
            TarifaBase = result.TarifaBaseCliente,
            Empresa    = result.EmpresaCliente
        };
    } */
    
    public async Task<Usuario?> LoginAsync(string identificador, string contrasena)
    {
        var hashContrasena = HashMd5ComoGuid(contrasena);

        using var conn = _db.CreateConnection();
        var result = await conn.QueryFirstOrDefaultAsync<SpLoginResult>(
            "SELECT * FROM fn_LoginUsuario(@Identificador, @Contrasena)",
            new { Identificador = identificador, Contrasena = hashContrasena }
        );

        if (result is null || result.Exitoso == 0)
            return null;

        return new Usuario
        {
            Id         = result.Id,
            Alias      = result.Alias,
            Nombres    = result.Nombres,
            Apellidos  = result.Apellidos,
            Correo     = result.Correo,
            Telefono   = result.Telefono,
            Rol        = result.Rol,
            IdCliente  = result.IdCliente,
            TarifaKm   = result.TarifaKmCliente,
            TarifaBase = result.TarifaBaseCliente,
            Empresa    = result.EmpresaCliente,
            IdOperador = result.IdOperador,
            NroLicencia          = result.NroLicencia,
            FecVenLic            = result.FecVenLic.HasValue ? DateOnly.FromDateTime(result.FecVenLic.Value) : null,
            ServiciosCompletados = result.ServiciosCompletados,
        };
    }

    private static string HashMd5ComoGuid(string input)
    {
        var hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(input));
        return new Guid(hashBytes).ToString().ToUpper();
    }

    private class SpLoginResult
    {
        public int    Exitoso   { get; set; }
        public string Mensaje   { get; set; } = string.Empty;
        public int    Id        { get; set; }
        public string Alias     { get; set; } = string.Empty;
        public string Nombres   { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Correo    { get; set; } = string.Empty;
        public string? Telefono  { get; set; }
        public string  Rol       { get; set; } = string.Empty;
        public int?    IdCliente         { get; set; }
        public decimal? TarifaKmCliente  { get; set; }
        public decimal? TarifaBaseCliente { get; set; }
        public string?  EmpresaCliente    { get; set; }
        public int?     IdOperador        { get; set; }
        public string?  NroLicencia       { get; set; }
        public DateTime? FecVenLic        { get; set; }
        public int?     ServiciosCompletados { get; set; }
    }
}
