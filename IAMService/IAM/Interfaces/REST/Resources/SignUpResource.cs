namespace IAMService.IAM.Interfaces.REST.Resources;

/// <summary>
/// Represents the sign-up resource. 
/// </summary>
/// <param name="Username">
/// The username of the user.
/// </param>
/// <param name="Password">
/// The password of the user.
/// </param>
public record SignUpResource(string Username, string Password);