using System.Net.Mime;
using IAMService.Profiles.Domain.Model.Commands;
using IAMService.Profiles.Domain.Model.Queries;
using IAMService.Profiles.Domain.Services;
using IAMService.Profiles.Interfaces.REST.Resources;
using IAMService.Profiles.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IAMService.Profiles.Interfaces;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Profile Endpoints.")]
public class ProfilesController(IProfileCommandService profileCommandService, IProfileQueryService profileQueryService) : ControllerBase
{
    
    [HttpGet("{profileId}")]
    [SwaggerOperation("Get Profile by ProfileId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profile found", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> GetProfileByProfileId(Guid profileId)
    {
       var getProfileByIdQuery = new GetProfileByIdQuery(profileId); 
       
       
       var profile = await profileQueryService.Handle(getProfileByIdQuery);
       
       if (profile is null) return BadRequest();
       var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
       
       return Ok(profileResource);
    }


    // POST -----------------------------------------------------
    // Create Profile by user Id 
    [HttpPost ("{userId}")]
    [SwaggerOperation("Create Profile by user Id")] 
    [SwaggerResponse(StatusCodes.Status201Created, "Profile created", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid Profile")]
    public async Task<IActionResult> CreateProfileAsync([FromBody] CreateProfileResource resource, int userId)
    {
        var createProfileCommand = CreateProfileCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var profile = await profileCommandService.Handle(createProfileCommand, userId);
        
        if (profile is null) return BadRequest();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        return CreatedAtAction(nameof(GetProfileByProfileId), new { profileId = profile.Id }, profileResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Profiles")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profiles found", typeof(IEnumerable<ProfileResource>))]
    public async Task<IActionResult> GetAllProfilesAsync()
    {
        var getAllProfilesQuery = new GetAllProfilesQuery();
        var profiles = await profileQueryService.Handle(getAllProfilesQuery);
        
        var profileResources = profiles.Select(ProfileResourceFromEntityAssembler.ToResourceFromEntity);
        
        return Ok(profileResources);
    }
    

    [HttpDelete("{profileId}")] 
    [SwaggerOperation("Delete Profile")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profile deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> DeleteProfile(Guid profileId)
    {
        var deleteProfileCommand = new DeleteProfileCommand(profileId);
        var result = await profileCommandService.Handle(deleteProfileCommand);
        
        //if profile not found
        if (!result) return BadRequest();
        
        return Ok();
    }
    
    //Get Profile by User Id
    [HttpGet("user/{userId}")]
    [SwaggerOperation("Get Profile by User Id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Profile found", typeof(ProfileResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Profile not found")]
    public async Task<IActionResult> GetProfileByUserId(int userId)
    {
        var getProfileByUserIdQuery = new GetProfileByUserIdQuery(userId);
        var profile = await profileQueryService.Handle(getProfileByUserIdQuery);
        
        if (profile is null) return BadRequest();
        var profileResource = ProfileResourceFromEntityAssembler.ToResourceFromEntity(profile);
        
        return Ok(profileResource);
    }
    
    
    
    
    
    
}