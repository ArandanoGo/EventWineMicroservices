namespace IAMService.Profiles.Interfaces.REST.Resources;

public record CreateProfileResource(
    string FirstName, 
    string LastName, 
    string Email, 
    string CompanyName, 
    string PhoneNumber, 
    string RUC, 
    string Street, 
    string Number, 
    string City, 
    string Country);