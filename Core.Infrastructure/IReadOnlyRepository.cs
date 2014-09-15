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
    /// This repository it is only for boost entity fermorwork performance 
    /// which will use No tracking..
    /// bear in mind when  using this repository you will need to attach it if you want to add/update/delete
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IReadOnlyRepository<T> : IDisposable where T : EntityBase
    {
        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

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
    }
}
