using CsPharma_V4.Areas.Identity.Data;
using CsPharma_V4.Core.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Pages.GestionUsuario.Repositories
{
    /*
     * Repositorio donde tendrémos todos los métodos necesarios relacionados con los roles
     * para implementar nuestro CRUD DE USUARIOS 
     */
    public class RoleRepository : IIdentityRoleRepository //Implementamos la interfaz previa para trabajar con el Identity
    {
        //Variable y constructor de inicialización del contexto (Con esto Accedemos a nuestra base de datos)
        private readonly LoginContexto _loginContexto;
        public RoleRepository(LoginContexto loginContexto)
        {
            _loginContexto = loginContexto;
        }

        //Devuelve una lista de todos los roles del sistema
        public ICollection<IdentityRole> GetRoles()
        {
            return _loginContexto.Roles.ToList();
        }
    }
}
