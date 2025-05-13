using IAMService.Profiles.Domain.Model.Commands;
using IAMService.Profiles.Interfaces.REST.Resources;

namespace IAMService.Profiles.Interfaces.REST.Transform;

public static class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        return new CreateProfileCommand(
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.CompanyName,
            resource.PhoneNumber,
            resource.RUC,
            resource.Street,
            resource.Number,
            resource.City,
            resource.Country
        );
    }
    
}

