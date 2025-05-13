using IAMService.Profiles.Domain.Model.Aggregate;
using IAMService.Profiles.Domain.Model.Commands;

namespace IAMService.Profiles.Domain.Services;


/// <summary>
/// Profile command service interface 
/// </summary>
public interface IProfileCommandService
{
    /// <summary>
    /// Handle create profile command 
    /// </summary>
    /// <param name="command">
    /// The <see cref="CreateProfileCommand"/> command
    /// </param>
    /// <returns>
    /// The <see cref="Profile"/> object with the created profile
    /// </returns>
    Task<Profile?> Handle(CreateProfileCommand command, int userId);
    
    Task<bool> Handle(DeleteProfileCommand command);
}