using System.Net.Mime;
using IAMService.IAM.Domain.Services;
using IAMService.IAM.Infrastructure.Pipeline.Middleware.Attributes;
using IAMService.IAM.Interfaces.REST.Resources;
using IAMService.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IAMService.IAM.Interfaces.REST;

/// <summary>
/// Represents the authentication controller. 
/// </summary>
/// <param name="userCommandService">
/// The <see cref="IUserCommandService"/> service.
/// </param>
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication Endpoints")]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    /// <summary>
    /// Signs in the user. 
    /// </summary>
    /// <param name="resource">
    /// The <see cref="SignInResource"/> resource.
    /// </param>
    /// <returns>
    /// The <see cref="AuthenticatedUserResource"/> resource.
    /// </returns>
    [AllowAnonymous]
    [HttpPost("sign-in")]
    [SwaggerOperation(
        Summary = "Signs in the user.",
        Description = "Signs in the user.",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated.", typeof(AuthenticatedUserResource))]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);
        var authenticatedUserResource =
            AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser.user,
                authenticatedUser.token);
        return Ok(authenticatedUserResource);
    }
    
    /// <summary>
    /// Signs up the user. 
    /// </summary>
    /// <param name="resource">
    /// The <see cref="SignUpResource"/> resource.
    /// </param>
    /// <returns>
    /// A message indicating that the user was signed up.
    /// </returns>
    [AllowAnonymous]
    [HttpPost("sign-up")]
    [SwaggerOperation(
        Summary = "Signs up the user.",
        Description = "Signs up the user.",
        OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was signed up.")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        await userCommandService.Handle(signUpCommand);
        
       
        return Ok(new { message = "User signed up successfully." });
    }
}