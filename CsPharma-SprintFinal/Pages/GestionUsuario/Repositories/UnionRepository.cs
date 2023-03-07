using CsPharma_V4.Core.Repositories;

namespace Pages.GestionUsuario.Repositories
{
    public class Union : IUnionRepository
    {
        public IUserRepository User { get; }

        public IRoleRepository Role { get; }

        public Union(IUserRepository user, IRoleRepository role)
        {
            User = user;
            Role = role;
        }
    }

}
