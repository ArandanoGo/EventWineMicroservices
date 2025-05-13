namespace IAMService.Profiles.Domain.Model.Commands;

public record CreateProfileCommand(
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