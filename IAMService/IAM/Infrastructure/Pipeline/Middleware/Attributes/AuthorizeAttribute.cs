using IAMService.IAM.Domain.Model.Aggregates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IAMService.IAM.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
/// Authorize Attribute 
/// </summary>
/// <remarks>
/// This attribute is used to authorize access to the endpoint.
/// It is used to decorate the endpoint and actions that require authentication. 
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    /// On Authorization
    /// </summary>
    /// <remarks>
    /// This method is called when the authorization filter is executed.
    /// It checks if the user is authenticated and if not, it returns an unauthorized result. 
    /// </remarks>
    /// <param name="context">
    /// The <see cref="AuthorizationFilterContext"/> Authorization Filter Context
    /// </param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
        {
            Console.WriteLine("Allow Anonymous");
            return;
        }
        
        var user = (User?)context.HttpContext.Items["User"];
        if (user is null) context.Result = new UnauthorizedResult();
    }
}