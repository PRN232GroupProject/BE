using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Interfaces
{
    public interface IGenericRepository<T>  where T : class
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
