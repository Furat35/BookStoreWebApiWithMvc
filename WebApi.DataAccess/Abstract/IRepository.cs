using System.Linq.Expressions;

namespace WebApi.DataAccess.Abstract
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> GetByGuidAsync(Guid id);
        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void SafeDelete(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
    }
}
