using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    /// <summary>
    /// IPersistenceService
    /// </summary>
    public interface IPersistenceService : IDisposable
    {
        /// <summary>
        /// GetRepository
        /// </summary>
        /// <typeparam name="T">T </typeparam>
        /// <returns>IRepository</returns>
        IRepository<T> GetRepository<T>() where T : EntityBase;
        
        /// <summary>
        /// Get ready only repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IReadOnlyRepository<T> GetReadyOnlyRepository<T>() where T : EntityBase;
    }
}
