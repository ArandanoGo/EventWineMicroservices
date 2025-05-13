using System.Text.Json.Serialization;
using IAMService.Profiles.Domain.Model.Aggregate;

namespace IAMService.IAM.Domain.Model.Aggregates;

/// <summary>
/// Represents a user in the system. 
/// </summary>
/// <param name="username">
/// The username of the user.
/// </param>
/// <param name="passwordHash">
/// The password hash of the user.
/// </param>
public class User(string username, string passwordHash)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class. 
    /// </summary>
    public User() : this(string.Empty, string.Empty) {}
    
    // Relación uno a muchos con la entidad Profile
    public Profile Profile { get; } // Relación uno a muchos con la entidad Profile
    
    public int Id { get; }

    public string Username { get; private set; } = username;
    
    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;
    
    /// <summary>
    /// Updates the username of the user. 
    /// </summary>
    /// <param name="username">
    /// The new username of the user.
    /// </param>
    /// <returns>
    /// The updated <see cref="User"/> user.
    /// </returns>
    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }
    
    /// <summary>
    /// Updates the password hash of the user. 
    /// </summary>
    /// <param name="passwordHash">
    /// The new password hash of the user.
    /// </param>
    /// <returns>
    /// The updated <see cref="User"/> user.
    /// </returns>
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
}