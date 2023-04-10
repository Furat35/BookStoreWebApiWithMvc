using WebApi.Core.Entities.Abstract;
using WebApi.DataAccess.Abstract;
using WebApi.DataAccess.Concrete;
using WebApi.DataAccess.EfDataAccess.Context;

namespace WebApi.DataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly EfContext _context;
        public UnitOfWork(EfContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public IRepository<T> GetRepository<T>() where T : class, IBaseEntity, new()
        {
            return new Repository<T>(_context);
        }
    }
}
