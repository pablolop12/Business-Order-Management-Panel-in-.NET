namespace CsPharma_V4.Core.Repositories
{
    //Interfaz del Repositorio UnitOfWork
    public interface IUnionRepository
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
    }
}
