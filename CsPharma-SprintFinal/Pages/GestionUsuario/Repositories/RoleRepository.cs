using CsPharma_V4.Areas.Identity.Data;
using CsPharma_V4.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Pages.GestionUsuario.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly LoginContexto _loginContexto;

        public RoleRepository(LoginContexto loginContexto)
        {
            _loginContexto = loginContexto;
        }

        public ICollection<IdentityRole> GetRoles()
        {
            return _loginContexto.Roles.ToList();
        }
    }
}
