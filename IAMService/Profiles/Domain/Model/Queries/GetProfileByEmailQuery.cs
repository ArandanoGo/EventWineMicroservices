using IAMService.Profiles.Domain.Model.ValueObjects;

namespace IAMService.Profiles.Domain.Model.Queries;

public record GetProfileByEmailQuery(EmailAddress Email);