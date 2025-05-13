using IAMService.IAM.Domain.Model.Aggregates;
using IAMService.IAM.Domain.Repositories;
using IAMService.Shared.Infrastructure.Persistence.EFC.Configuration;
using IAMService.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IAMService.IAM.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// User repository implementation. 
/// </summary>
/// <remarks>
/// This class is responsible for implementing the user repository interface.
/// </remarks>
/// <param name="context">
/// The <see cref="AppDbContext"/> application database context.
/// </param>
public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    // inheritedDoc
    public async Task<User?> FindByUsernameAsync(string username)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.Username.Equals(username));
    }

    // inheritedDoc
    public bool ExistsByUsername(string username)
    {
        return Context.Set<User>().Any(user => user.Username.Equals(username));
    }
}