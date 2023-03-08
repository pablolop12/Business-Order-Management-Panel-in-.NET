using Microsoft.AspNetCore.Identity;

namespace CsPharma_V4.Core.Repositories
{
    //Interfaz que sirve para trabajar con IdentityRole en la clase que lo implemente
    public interface IIdentityRoleRepository
    {
        ICollection<IdentityRole> GetRoles();//Devuelve una coleccion de los Roles asignados del sistema
    }
}
