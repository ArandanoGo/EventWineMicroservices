using IAMService.Profiles.Domain.Model.Aggregate;
using IAMService.Profiles.Domain.Model.ValueObjects;
using IAMService.Profiles.Domain.Repositories;
using IAMService.Shared.Infrastructure.Persistence.EFC.Configuration;
using IAMService.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IAMService.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository(AppDbContext context) : BaseRepository<Profile>(context), IProfileRepository
{
    
    /// <inheritdoc />
    public async Task<Profile?> FindProfileByEmailAsync(EmailAddress email)
    {
        return Context.Set<Profile>().FirstOrDefault(p => p.Email == email);
    }
    
    public async Task<Profile?> GetProfileByIdAsync(Guid Id)
    {
        return await Context.Set<Profile>().FirstOrDefaultAsync(p => p.Id == Id);
    }
    
    public async Task<Profile?> GetProfileByUserIdAsync(int userId)
    {
        return await Context.Set<Profile>().FirstOrDefaultAsync(p => p.UserId == userId);
    }

    public async Task<bool?> existsProfileByEmailAsync(string email)
    {
        return await Context.Set<Profile>().AnyAsync(p => p.EmailAddress == email);
    }
    

    public async  Task<bool?> ExistsProfileByRUCAsync(string ruc)
    {
        return await Context.Set<Profile>().AnyAsync(p => p.RUC == ruc);
    }
}