namespace IAMService.IAM.Interfaces.REST.Resources;

/// <summary>
/// Represents the sign-in resource. 
/// </summary>
/// <param name="Username">
/// The username of the user.
/// </param>
/// <param name="Password">
/// The password of the user.
/// </param>
public record SignInResource(string Username, string Password);