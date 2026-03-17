using System;
using System.Net.Http;
using System.Text.Json;

namespace OlycksRapporteringV2.MAUI.Services
{
    public class WeatherService
    {
        private readonly HttpClient _client;

        public WeatherService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            _client = new HttpClient(handler);
            _client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
        }

        //HÄMTA KOORDINATER FÖR EN PLATS — returnerar strängar med punkt som decimal\\
        public async Task<(string lat, string lon)?> GetCoordinates(string location)
        {
            try
            {
                var url = $"https://geocoding-api.open-meteo.com/v1/search?name={Uri.EscapeDataString(location)}&count=1&language=sv";
                System.Diagnostics.Debug.WriteLine($"Koordinat-anrop: {url}");
                var response = await _client.GetStringAsync(url);
                var json = JsonDocument.Parse(response);

                if (!json.RootElement.TryGetProperty("results", out var results))
                    return null;

                var first = results[0];

                //KONVERTERA MED INVARIANTCULTURE SÅ DET BLIR PUNKT INTE KOMMA\\
                var lat = first.GetProperty("latitude").GetDouble()
                    .ToString(System.Globalization.CultureInfo.InvariantCulture);
                var lon = first.GetProperty("longitude").GetDouble()
                    .ToString(System.Globalization.CultureInfo.InvariantCulture);

                return (lat, lon);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Koordinat-FEL: {ex.Message}");
                return null;
            }
        }

        //HÄMTA VÄDER FÖR PLATS OCH DATUM\\
        public async Task<(string description, double temp, double wind)?> GetWeatherForLocation(string location, string dateTimeString)
        {
            try
            {
                //PARSA DATUMET FRÅN RAPPORTEN\\
                var formats = new[] { "yyyy-MM-dd HH:mm", "yyyyMMdd HH:mm", "d/M-yy HH:mm" };
                if (!DateTime.TryParseExact(dateTimeString?.Trim(), formats,
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime accidentDate))
                    return null;

                //HÄMTA KOORDINATER\\
                var coords = await GetCoordinates(location);
                if (coords == null) return null;

                var latStr = coords.Value.lat; // redan sträng med punkt
                var lonStr = coords.Value.lon;
                var dateStr = accidentDate.ToString("yyyy-MM-dd");
                var hour = accidentDate.Hour;

                //VÄLJ RÄTT API BEROENDE PÅ DATUM\\
                string url;
                if (accidentDate.Date < DateTime.Today)
                {
                    //HISTORISKT VÄDER\\
                    url = $"https://archive-api.open-meteo.com/v1/archive?latitude={latStr}&longitude={lonStr}&start_date={dateStr}&end_date={dateStr}&hourly=temperature_2m,wind_speed_10m,weather_code&wind_speed_unit=kmh";
                }
                else
                {
                    //AKTUELLT/PROGNOS\\
                    url = $"https://api.open-meteo.com/v1/forecast?latitude={latStr}&longitude={lonStr}&hourly=temperature_2m,wind_speed_10m,weather_code&wind_speed_unit=kmh&start_date={dateStr}&end_date={dateStr}";
                }

                System.Diagnostics.Debug.WriteLine($"Väder-anrop: {url}");
                var response = await _client.GetStringAsync(url);
                var json = JsonDocument.Parse(response);

                var hourly = json.RootElement.GetProperty("hourly");
                var temp = hourly.GetProperty("temperature_2m")[hour].GetDouble();
                var wind = hourly.GetProperty("wind_speed_10m")[hour].GetDouble();
                var code = hourly.GetProperty("weather_code")[hour].GetInt32();

                return (WeatherCodeToDescription(code), temp, wind);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Väder-FEL: {ex.Message}");
                return null;
            }
        }

        //OMVANDLA VÄDERKOD TILL BESKRIVNING\\
        private string WeatherCodeToDescription(int code) => code switch
        {
            0 => "Klart",
            1 or 2 or 3 => "Delvis molnigt",
            45 or 48 => "Dimma",
            51 or 53 or 55 => "Duggregn",
            61 or 63 or 65 => "Regn",
            71 or 73 or 75 => "Snöfall",
            80 or 81 or 82 => "Regnskurar",
            95 => "Åskväder",
            _ => "Okänt väder"
        };
    }
}