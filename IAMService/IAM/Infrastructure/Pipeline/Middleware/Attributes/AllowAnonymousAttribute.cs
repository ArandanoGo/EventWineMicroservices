namespace IAMService.IAM.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
/// Allow Anonymous Attributes 
/// </summary>
/// <remarks>
/// This attribute is used to allow anonymous access to the endpoint.
/// It is used to decorate the endpoint and actions that do not require authentication.
/// </remarks>
[AttributeUsage(AttributeTargets.Method)]
public class AllowAnonymousAttribute : Attribute
{
    
}