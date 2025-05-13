using IAMService.Profiles.Domain.Model.Aggregate;
using IAMService.Profiles.Domain.Model.Queries;
using IAMService.Profiles.Domain.Repositories;
using IAMService.Profiles.Domain.Services;

namespace IAMService.Profiles.Application.Internal.QueryServices;


/// <summary>
/// Profile query service 
/// </summary>
/// <param name="profileRepository">
/// Profile repository
/// </param>
public class ProfileQueryService(IProfileRepository profileRepository) : IProfileQueryService
{
    /// <inheritdoc />
    public async  Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await profileRepository.ListAsync();
    }
    
    /// <inheritdoc />
    public async Task<Profile?> Handle(GetProfileByEmailQuery query)
    {
        return await profileRepository.FindProfileByEmailAsync(query.Email);
    }
    
    /// <inheritdoc />
    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.GetProfileByIdAsync(query.Id);
    }
    
    public async Task<Profile?> Handle(GetProfileByUserIdQuery query)
    {
        return await profileRepository.GetProfileByUserIdAsync(query.UserId);
    }

}