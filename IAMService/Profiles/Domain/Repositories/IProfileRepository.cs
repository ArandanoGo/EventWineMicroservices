using IAMService.Profiles.Domain.Model.Aggregate;
using IAMService.Profiles.Domain.Model.ValueObjects;
using IAMService.Shared.Domain.Repositories;

namespace IAMService.Profiles.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile>
{
    Task<Profile?> FindProfileByEmailAsync(EmailAddress email);
    
    Task<Profile?> GetProfileByIdAsync(Guid Id);
    
    Task<Profile?> GetProfileByUserIdAsync(int userId);
    
    Task<bool?> existsProfileByEmailAsync(string email);
    
    Task<bool?> ExistsProfileByRUCAsync(string ruc);
    
}