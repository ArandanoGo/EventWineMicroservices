using System.Net.Mime;
using IAMService.IAM.Domain.Model.Queries;
using IAMService.IAM.Domain.Services;
using IAMService.IAM.Interfaces.REST.Resources;
using IAMService.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IAMService.IAM.Interfaces.REST;

/// <summary>
/// User Controller 
/// </summary>
/// <param name="userQueryService">
/// The <see cref="IUserQueryService"/> user query service
/// </param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User Endpoints")]
public class UserController(IUserQueryService userQueryService) : ControllerBase
{
    /// <summary>
    /// Get User by its Id 
    /// </summary>
    /// <param name="id">
    /// The User Id
    /// </param>
    /// <returns>
    /// The <see cref="Task{IActionResult}"/> containing the <see cref="UserResource"/> if found, otherwise NotFound
    /// </returns>
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get User by its Id",
        Description = "Get User by Id",
        OperationId = "GetUserById")]
    [SwaggerResponse(StatusCodes.Status200OK, "User found", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery);
        if (user is null) return NotFound();
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        return Ok(userResource);
    }

    /// <summary>
    /// Get All Users 
    /// </summary>
    /// <returns>
    /// The <see cref="Task{IActionResult}"/> containing the <see cref="IEnumerable{UserResource}"/> if found, otherwise an empty list
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Users",
        Description = "Get All Users",
        OperationId = "GetAllUsers")]
    [SwaggerResponse(StatusCodes.Status200OK, "Users found", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUsersQuery = new GetAllUsersQuery();
        var users = await userQueryService.Handle(getAllUsersQuery);
        var userResources = users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(userResources);
    }
    
    [ApiController]
    [Route("iam")]
    public class HealthController : ControllerBase
    {
        [HttpGet("status")]
        [HttpGet("/status")]
        public IActionResult Status()
        {
            return Ok("IAM Service is alive");
        }
    }

}