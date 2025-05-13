using System.Text.Json;
using AdminService.API.Models;

namespace AdminService.API.Services;

public class ServiceMonitor
{
    private readonly HttpClient _http = new();

    public async Task<List<ServiceStatus>> CheckAllAsync()
    {
        var registryUrl = "http://localhost:5002/registry/services"; // ← CAMBIA el puerto

        var response = await _http.GetAsync(registryUrl);
        var services = await JsonSerializer.DeserializeAsync<List<ServiceStatus>>(
            await response.Content.ReadAsStreamAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        );

        if (services is null)
            return new();

        var results = new List<ServiceStatus>();

        foreach (var service in services)
        {
            var alive = await IsServiceAlive(service.Url);
            results.Add(new ServiceStatus
            {
                Name = service.Name,
                Url = service.Url,
                IsAlive = alive
            });
        }

        return results;
    }

    private async Task<bool> IsServiceAlive(string url)
    {
        try
        {
            var result = await _http.GetAsync(url + "/status");
            return result.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }
}