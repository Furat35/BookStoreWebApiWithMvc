using WebApi.Core.Entities.Abstract;
using WebApi.DataAccess.Abstract;

namespace WebApi.DataAccess.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync();
        int Save();
        IRepository<T> GetRepository<T>() where T : class, IBaseEntity, new();
    }
}
