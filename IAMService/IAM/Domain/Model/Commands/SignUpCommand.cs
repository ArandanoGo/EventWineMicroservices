namespace IAMService.IAM.Domain.Model.Commands;

/// <summary>
/// Command to sign up a new user 
/// </summary>
/// <param name="Username">
/// The username of the user to sign up
/// </param>
/// <param name="Password">
/// The password of the user to sign up
/// </param>
public record SignUpCommand(string Username, string Password);