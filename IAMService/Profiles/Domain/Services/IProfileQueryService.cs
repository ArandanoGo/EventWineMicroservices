using IAMService.Profiles.Domain.Model.Aggregate;
using IAMService.Profiles.Domain.Model.Queries;

namespace IAMService.Profiles.Domain.Services;

/// <summary>
/// Profile query service 
/// </summary>
public interface IProfileQueryService
{
    /// <summary>
    /// Handle get all profiles 
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetAllProfilesQuery"/> query
    /// </param>
    /// <returns>
    /// A list of <see cref="Profile"/> objects
    /// </returns>
    Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query);
    
    /// <summary>
    /// Handle get profile by email 
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetProfileByEmailQuery"/> query
    /// </param>
    /// <returns>
    /// A <see cref="Profile"/> object or null
    /// </returns>
    Task<Profile?> Handle(GetProfileByEmailQuery query);
    
    
    Task<Profile?> Handle(GetProfileByIdQuery query);
    
    
    Task<Profile?> Handle(GetProfileByUserIdQuery query);
    
}