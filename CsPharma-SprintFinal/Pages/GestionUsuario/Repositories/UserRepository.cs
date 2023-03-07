using CsPharma_V4.Areas.Identity.Data;
using CsPharma_V4.Core.Repositories;

namespace Pages.GestionUsuario.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LoginContexto _loginContexto;

        public UserRepository(LoginContexto loginContexto)
        {
            _loginContexto = loginContexto;
        }

        public ICollection<User> GetUsers()
        {
            return _loginContexto.Users.ToList();
        }

        public User GetUser(string id)
        {
            return _loginContexto.Users.FirstOrDefault(u => u.Id == id);
        }

        public User UpdateUser(User user)
        {
            _loginContexto.Update(user);
            _loginContexto.SaveChanges();

            return user;
        }
    }
}
