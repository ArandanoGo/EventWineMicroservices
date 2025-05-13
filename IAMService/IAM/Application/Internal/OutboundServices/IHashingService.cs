namespace IAMService.IAM.Application.Internal.OutboundServices;

/// <summary>
/// Hashing service interface 
/// </summary>
public interface IHashingService
{
    /// <summary>
    /// Hash password 
    /// </summary>
    /// <param name="password">
    /// Password to hash
    /// </param>
    /// <returns>
    /// A string representing the hashed password
    /// </returns>
    string HashPassword(string password);

    /// <summary>
    /// Verify password 
    /// </summary>
    /// <param name="password">
    /// Password to verify
    /// </param>
    /// <param name="passwordHash">
    /// Hashed password to verify against
    /// </param>
    /// <returns>
    /// A boolean indicating if the password is valid
    /// </returns>
    bool VerifyPassword(string password, string passwordHash);
}