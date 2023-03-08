using CsPharma_V4.Core.Repositories;

namespace Pages.GestionUsuario.Repositories
{
    /*
    * Repositorio que sirve para agrupar el repositorio de Roles e Interfaces en uno solo.
    */
    public class Union : IUnionRepository
    {
        public IUserRepository User { get; }

        public IIdentityRoleRepository Role { get; }

        public Union(IUserRepository user, IIdentityRoleRepository role)
        {
            User = user;
            Role = role;
        }
    }

}
