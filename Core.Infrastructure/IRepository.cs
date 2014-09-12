using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    /// <summary>
    /// IRepository
    /// </summary>
    /// <typeparam name="T">T</typeparam>
    public interface IRepository<T> : IDisposable where T : EntityBase
    {
        /// <summary>
        /// Find 
        /// </summary>
        /// <param name="keyValues">keyValues</param>
        /// <returns>T</returns>
        T Find(params object[] keyValues);

        /// <summary>
        /// FindAsync
        /// </summary>
        /// <param name="keyValues">keyValues</param>
        /// <returns>T</returns>
        Task<T> FindAsync(params object[] keyValues);

        /// <summary>
        /// FindAsync
        /// </summary>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <param name="keyValues">keyValues</param>
        /// <returns>Task<T></returns>
        Task<T> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// FindBy
        /// </summary>
        /// <param name="express">express</param>
        /// <returns>T</returns>
        IQueryable<T> FindBy(Expression<Func<T, bool>> express);

        /// <summary>
        /// SqlQuery
        /// </summary>
        /// <param name="query">query</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        IQueryable<T> SqlQuery(string query, params object[] parameters);

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="entity">entity</param>
        void Insert(T entity);

        /// <summary>
        /// InsertRange
        /// </summary>
        /// <param name="entities">entities</param>
        void InsertRange(IEnumerable<T> entities);

        /// <summary>
        /// InsertGraph
        /// </summary>
        /// <param name="entity">entity</param>
        void InsertGraph(T entity);

        /// <summary>
        /// InsertGraphRange
        /// </summary>
        /// <param name="entities"></param>
        void InsertGraphRange(IEnumerable<T> entities);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">entity</param>
        void Update(T entity);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id">id</param>
        void Delete(object id);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="entity">entity</param>
        void Delete(T entity);
    }
}
