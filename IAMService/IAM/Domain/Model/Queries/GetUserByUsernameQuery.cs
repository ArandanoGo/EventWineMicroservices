namespace IAMService.IAM.Domain.Model.Queries;

/// <summary>
/// Get user by username query 
/// </summary>
/// <param name="Username">
/// The username of the user
/// </param>
public record GetUserByUsernameQuery(string Username);