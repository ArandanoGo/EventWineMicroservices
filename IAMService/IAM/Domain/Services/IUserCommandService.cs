using IAMService.IAM.Domain.Model.Aggregates;
using IAMService.IAM.Domain.Model.Commands;

namespace IAMService.IAM.Domain.Services;

/// <summary>
/// User command service
/// </summary>
/// <remarks>
/// This service is responsible for handling user commands
/// </remarks>
public interface IUserCommandService
{
    /// <summary>
    /// Handle sign up command 
    /// </summary>
    /// <param name="command">
    /// The <see cref="SignUpCommand"/> sign-up command containing user information
    /// </param>
    /// <returns>
    /// A task representing the operation
    /// </returns>
    Task Handle(SignUpCommand command);
    
    /// <summary>
    /// Handle sign-in command 
    /// </summary>
    /// <param name="command">
    /// The <see cref="SignInCommand"/> sign-in command containing user information
    /// </param>
    /// <returns></returns>
    Task<(User user, string token)> Handle(SignInCommand command);

}