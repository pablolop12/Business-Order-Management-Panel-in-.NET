using Microsoft.AspNetCore.Identity;

namespace CsPharma_V4.Core.Repositories
{
    public interface IRoleRepository
    {
        ICollection<IdentityRole> GetRoles();
    }
}
