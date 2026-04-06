using System.Text;
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
        var url = "https://routes.googleapis.com/directions/v2:computeRoutes";

        var body = new
        {
            origin = new
            {
                location = new
                {
                    latLng = new
                    {
                        latitude  = double.Parse(origenLat,  System.Globalization.CultureInfo.InvariantCulture),
                        longitude = double.Parse(origenLon,  System.Globalization.CultureInfo.InvariantCulture)
                    }
                }
            },
            destination = new
            {
                location = new
                {
                    latLng = new
                    {
                        latitude  = double.Parse(destinoLat, System.Globalization.CultureInfo.InvariantCulture),
                        longitude = double.Parse(destinoLon, System.Globalization.CultureInfo.InvariantCulture)
                    }
                }
            },
            travelMode = "DRIVE"
        };

        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
        };
        request.Headers.Add("X-Goog-Api-Key",    _apiKey);
        request.Headers.Add("X-Goog-FieldMask", "routes.duration,routes.distanceMeters");

        var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        var doc  = JsonDocument.Parse(json);
        var root = doc.RootElement;

        if (!root.TryGetProperty("routes", out var routes)) return null;
        if (routes.GetArrayLength() == 0) return null;

        var route = routes[0];
        if (!route.TryGetProperty("distanceMeters", out var distProp)) return null;
        if (!route.TryGetProperty("duration",       out var durProp))  return null;

        var distanciaM = distProp.GetInt32();
        var duracionStr = durProp.GetString() ?? "0s";
        var duracionS  = int.Parse(duracionStr.TrimEnd('s'));

        return (Math.Round(distanciaM / 1000m, 2), (int)Math.Ceiling(duracionS / 60.0));
    }
}
