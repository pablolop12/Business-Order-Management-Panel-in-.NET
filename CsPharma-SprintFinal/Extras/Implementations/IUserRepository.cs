
using CsPharma_V4.Areas.Identity.Data;

namespace CsPharma_V4.Core.Repositories
{
    //Interfaz del Repositorio UserRepository
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(string id);
        User UpdateUser(User user);
    }

}

