using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Repository.Context;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository.Repositories.Implements
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ChemProjectDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;


        public GenericRepository(ChemProjectDbContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
        }
        public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null) query = include(query);


            if (predicate != null) query = query.Where(predicate);


            if (orderBy != null) return await orderBy(query).FirstOrDefaultAsync();


            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int? take = null)
        {
            IQueryable<T> query = _dbSet;


            if (include != null) query = include(query);


            if (predicate != null) query = query.Where(predicate);


            if (orderBy != null) query = orderBy(query);


            if (take.HasValue) query = query.Take(take.Value);


            return await query.ToListAsync();
        }


        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null) return await _dbSet.CountAsync(predicate);
            return await _dbSet.CountAsync();
        }


        public virtual async Task InsertAsync(T entity)
        {
            if (entity == null) return;
            await _dbSet.AddAsync(entity);
        }


        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }


        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }


        public virtual IQueryable<T> CreateBaseQuery(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool asNoTracking = true)
        {
            IQueryable<T> query = _dbSet;


            if (include != null)
                query = include(query);


            if (predicate != null)
                query = query.Where(predicate);


            if (asNoTracking)
                query = query.AsNoTracking();


            return query;
        }
    }
}
