namespace CsPharma_V4.Core.Repositories
{
    //Esta interfaz actua como contenedor para trabajar con los otros dos repositorios
    //Roles y User, ya que en nuestra base de datos no se encuentran en la misma localización.

    //Esta interfaz sirve como unión para todas aquellas clases que necesiten interactuar con
    //los roles o con los usuarios
    public interface IUnionRepository
    {
        IUserRepository User { get; }
        IIdentityRoleRepository Role { get; }
    }
}
