
using CsPharma_V4.Areas.Identity.Data;

namespace CsPharma_V4.Core.Repositories
{
    //Interfaz del Repositorio UserRepository
    //Contiene una coleccion y dos métodos, para trabajar con los registros de usuario
    public interface IUserRepository
    {
        ICollection<User> GetUsers();//Devuelve una coleccion de los Usuarios del sistema
        User GetUser(string id); //Devuelve un usuario en base a su ID
        User UpdateUser(User user); //Actualizará los datos del usuario que reciba
    }

}

