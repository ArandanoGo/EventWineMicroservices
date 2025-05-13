namespace AdminService.API.Models;

public class ServiceStatus
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsAlive { get; set; }
}