using IAMService.Profiles.Domain.Model.Aggregate;
using IAMService.Profiles.Domain.Model.Commands;
using IAMService.Profiles.Domain.Repositories;
using IAMService.Profiles.Domain.Services;
using IAMService.Shared.Domain.Repositories;

namespace IAMService.Profiles.Application.Internal.CommandServices;


/// <summary>
/// Profile command service 
/// </summary>
/// <param name="profileRepository">
/// Profile repository
/// </param>
/// <param name="unitOfWork">
/// Unit of work
/// </param>
public class ProfileCommandService(IProfileRepository profileRepository, IUnitOfWOrk unitOfWork) : IProfileCommandService
{
    
    /// <inheritdoc />
    public async Task<Profile?> Handle(CreateProfileCommand command, int userId)
    {
        var profile = new Profile(command, userId);

        //if ((bool)await profileRepository.existsProfileByEmailAsync(command.Email)) throw new Exception("Email already exists");
        
        if ((bool)await profileRepository.ExistsProfileByRUCAsync(command.RUC)) throw new Exception("RUC already exists");
        
        await profileRepository.AddAsync(profile);
        await unitOfWork.CompleteAsync();
        
        return profile;
    }
    
    /// <inheritdoc />
    public async Task<bool> Handle(DeleteProfileCommand command)
    {
        var profile = await profileRepository.GetProfileByIdAsync(command.Id);
        
        if (profile is null) return false;
        
        profileRepository.Remove(profile);
        await unitOfWork.CompleteAsync();
        return true;
    }
    
    
 
}