using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Entity;
using Core.Infrastructure;

namespace Core.Concrete
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>, IDisposable where TEntity:EntityBase 
    {
        private readonly IDataContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public ReadOnlyRepository(IDataContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }        

        public virtual IQueryable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).AsQueryable().AsNoTracking();
        }    


        internal IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includeProperties != null)
                includeProperties.ForEach(i => query = query.Include(i));

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            if (page != null && pageSize != null)
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);

            return query.AsNoTracking();
        }

        internal async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includeProperties = null,
            int? page = null,
            int? pageSize = null)
        {
            return Get(filter, orderBy, includeProperties, page, pageSize).AsEnumerable();
        }


        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> express)
        {
            return _dbSet.Where(express).AsNoTracking();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
