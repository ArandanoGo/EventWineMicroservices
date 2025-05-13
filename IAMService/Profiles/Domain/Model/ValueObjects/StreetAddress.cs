namespace IAMService.Profiles.Domain.Model.ValueObjects;

public record StreetAddress(string Street, string Number, string City, string Country)
{
    public StreetAddress() : this(string.Empty, string.Empty, string.Empty, string.Empty) { }
   
    public StreetAddress(string street) : this(street, string.Empty, string.Empty, string.Empty) { }
    
    public StreetAddress(string street, string city, string number) : this(street, number, city, string.Empty) { }
    public string FullAddress => $"{Street} {Number}, {City}, {Country}";
}