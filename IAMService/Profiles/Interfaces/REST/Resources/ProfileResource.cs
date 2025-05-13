namespace IAMService.Profiles.Interfaces.REST.Resources;

public record ProfileResource(
    Guid Id, 
    string FullName, 
    string Email, 
    string CompanyName,
    string PhoneNumber,
    string RUC,
    string StreetAddress);