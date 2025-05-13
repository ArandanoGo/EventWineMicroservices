namespace IAMService.IAM.Domain.Model.Commands;

/// <summary>
/// Command to sign in a user 
/// </summary>
/// <param name="Username">
/// The username of the user to sign in
/// </param>
/// <param name="Password">
/// The password of the user to sign in
/// </param>
public record SignInCommand(string Username, string Password);