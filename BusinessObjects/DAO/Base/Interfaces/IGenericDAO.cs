using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace BusinessObjects.DAO.Base.Interfaces
{
    public interface IGenericDAO<T> where T : class
    {
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);


        Task<ICollection<T>> GetListAsync(
        Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        int? take = null);


        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);


        Task InsertAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> CreateBaseQuery(Expression<Func<T, bool>> predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        bool asNoTracking = true);
    }
}
