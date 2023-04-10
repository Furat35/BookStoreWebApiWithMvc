using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApi.Core.Entities.Abstract;
using WebApi.DataAccess.Abstract;
using WebApi.DataAccess.EfDataAccess.Context;

namespace WebApi.DataAccess.Concrete
{
    public class Repository<T> : IRepository<T>
    where T : class, IBaseEntity, new()
    {
        protected EfContext _context;
        public Repository(EfContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAllAsync(Expression<Func<T, bool>> predicate = null, bool trackChanges = false)
        {
            var dataSet = _context.Set<T>();
            return predicate is null
                ? (trackChanges
                    ? dataSet
                    : dataSet.AsNoTracking())
                : (trackChanges
                    ? dataSet.Where(predicate)
                    : dataSet.AsNoTracking().Where(predicate));
        }

        public async Task<T> GetByGuidAsync(Guid id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate, bool trackChanges = false)
        {
            return await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public void Add(T entity)
        {
            _context.AddAsync(entity);
        }

        public void SafeDelete(T entity)
        {
            _context.Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return expression is null ? await _context.Set<T>().AsNoTracking().CountAsync() : await _context.Set<T>().Where(expression).AsNoTracking().CountAsync();
        }
    }
}
