using IAMService.IAM.Domain.Model.Aggregates;
using IAMService.IAM.Domain.Model.Queries;

namespace IAMService.IAM.Domain.Services;

/// <summary>
/// User query service 
/// </summary>
/// <remarks>
/// This service is responsible for handling user queries
/// </remarks>
public interface IUserQueryService
{
    /// <summary>
    /// Handle get user by id query 
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetUserByIdQuery"/> query containing user id
    /// </param>
    /// <returns>
    /// A task containing the <see cref="User"/> user if found, otherwise null
    /// </returns>
    Task<User?> Handle(GetUserByIdQuery query);
    
    /// <summary>
    /// Handle get all users query 
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetAllUsersQuery"/> query
    /// </param>
    /// <returns>
    /// A task containing the list of <see cref="User"/> users
    /// </returns>
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    
    /// <summary>
    /// Handle get user by username query 
    /// </summary>
    /// <param name="query">
    /// The <see cref="GetUserByUsernameQuery"/> query containing the username
    /// </param>
    /// <returns>
    /// A task containing the <see cref="User"/> user if found, otherwise null
    /// </returns>
    Task<User?> Handle(GetUserByUsernameQuery query);
}