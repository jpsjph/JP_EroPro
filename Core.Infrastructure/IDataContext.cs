using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    /// <summary>
    /// IDataContext
    /// </summary>
    public interface IDataContext : IDisposable
    {

        /// <summary>
        ///  Get Database
        /// </summary>
        Database DatabaseContext { get; }
    

        /// <summary>
        /// Set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        DbSet<T> Set<T>() where T : EntityBase;

        /// <summary>
        /// SaveChanges
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// SaveChangesAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// SaveChangesAsync
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// SyncObjectState
        /// </summary>
        /// <param name="entity"></param>
        void SyncObjectState(object entity);
    }
}
