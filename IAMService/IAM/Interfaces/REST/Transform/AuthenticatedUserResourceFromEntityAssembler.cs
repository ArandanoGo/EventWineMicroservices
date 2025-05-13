using IAMService.IAM.Domain.Model.Aggregates;
using IAMService.IAM.Interfaces.REST.Resources;

namespace IAMService.IAM.Interfaces.REST.Transform;

/// <summary>
/// Represents the assembler for the authenticated user resource from entity. 
/// </summary>
public static class AuthenticatedUserResourceFromEntityAssembler
{
    /// <summary>
    /// Converts the user entity to the authenticated user resource. 
    /// </summary>
    /// <param name="user">
    /// The user entity.
    /// </param>
    /// <param name="token">
    /// The token.
    /// </param>
    /// <returns>
    /// The <see cref="AuthenticatedUserResource"/> resource.
    /// </returns>
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.Username, token);
    }
}