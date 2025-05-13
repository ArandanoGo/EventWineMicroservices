using IAMService.IAM.Domain.Model.Aggregates;
using IAMService.Shared.Domain.Repositories;

namespace IAMService.IAM.Domain.Repositories;

/// <summary>
/// Represents the user repository.
/// </summary>
/// <remarks>
/// This interface is used to define the contract for the user repository.
/// </remarks>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Finds a user by username. 
    /// </summary>
    /// <param name="username">
    /// The username to search for.
    /// </param>
    /// <returns>
    /// The user if found; otherwise, null.
    /// </returns>
    Task<User?> FindByUsernameAsync(string username);
    
    /// <summary>
    /// Checks if a user with the specified username exists. 
    /// </summary>
    /// <param name="username">
    /// The username to check for.
    /// </param>
    /// <returns>
    /// True if the user exists; otherwise, false.
    /// </returns>
    bool ExistsByUsername(string username);
}