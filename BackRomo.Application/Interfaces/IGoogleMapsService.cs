namespace BackRomo.Application.Interfaces;

public interface IGoogleMapsService
{
    Task<(decimal distanciaKm, int tiempoMin)?> ObtenerDistanciaAsync(
        string origenLat, string origenLon,
        string destinoLat, string destinoLon);
}
