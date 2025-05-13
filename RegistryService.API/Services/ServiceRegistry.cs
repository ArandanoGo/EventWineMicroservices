using RegistryService.API.Models;

namespace RegistryService.API.Services;

public class ServiceRegistry
{
    private readonly Dictionary<string, ServiceInfo> _services = new();

    public void Register(ServiceInfo service)
    {
        _services[service.Name] = service;
    }

    public IEnumerable<ServiceInfo> GetAll()
    {
        return _services.Values;
    }

    public ServiceInfo? Get(string name)
    {
        _services.TryGetValue(name, out var service);
        return service;
    }
}