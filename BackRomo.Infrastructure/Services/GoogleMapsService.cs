using System.Text.Json;
using BackRomo.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BackRomo.Infrastructure.Services;

public class GoogleMapsService : IGoogleMapsService
{
    private readonly HttpClient _httpClient;
    private readonly string     _apiKey;

    public GoogleMapsService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey     = configuration["GoogleMaps:ApiKey"]!;
    }

    public async Task<(decimal distanciaKm, int tiempoMin)?> ObtenerDistanciaAsync(
        string origenLat, string origenLon,
        string destinoLat, string destinoLon)
    {
        var url = $"https://maps.googleapis.com/maps/api/directions/json" +
                  $"?origin={origenLat},{origenLon}" +
                  $"&destination={destinoLat},{destinoLon}" +
                  $"&mode=driving&key={_apiKey}";

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        var doc  = JsonDocument.Parse(json);
        var root = doc.RootElement;

        if (root.GetProperty("status").GetString() != "OK") return null;

        var routes = root.GetProperty("routes");
        if (routes.GetArrayLength() == 0) return null;

        var leg = routes[0].GetProperty("legs")[0];

        var distanciaM = leg.GetProperty("distance").GetProperty("value").GetInt32();
        var duracionS  = leg.GetProperty("duration").GetProperty("value").GetInt32();

        return (Math.Round(distanciaM / 1000m, 2), (int)Math.Ceiling(duracionS / 60.0));
    }
}
