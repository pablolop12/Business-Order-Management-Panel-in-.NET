using CsPharma_V4.Areas.Identity.Data;
using CsPharma_V4.Core.Repositories;

namespace Pages.GestionUsuario.Repositories
{   /*
     * Repositorio donde tendrémos todos los métodos necesarios relacionados con los roles
     * para implementar nuestro CRUD DE USUARIOS 
     */
    public class UserRepository : IUserRepository //Implementamos la Interfaz de IUserRepository anterior
    {
        //Variable y constructor de inicialización del contexto (Con esto Accedemos a nuestra base de datos)
        private readonly LoginContexto _loginContexto;
        public UserRepository(LoginContexto loginContexto)
        {
            _loginContexto = loginContexto;
        }

        //Devuelve una lista de todos los usuarios
        public ICollection<User> GetUsers()
        {
            return _loginContexto.Users.ToList();
        }


        //Devuelve un usuario a partir de la ID que se le proporcione
        public User GetUser(string id)
        {
            return _loginContexto.Users.FirstOrDefault(u => u.Id == id);
        }


        //Actualizará el usuario que se le proporcione
        public User UpdateUser(User user)
        {
            _loginContexto.Update(user);
            _loginContexto.SaveChanges();

            return user;
        }
    }
}
