using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concrete
{
    public class PersistenceService : IPersistenceService
    {
        private IDataContext _context;
        public PersistenceService(IDataContext context)
        {
            _context = context;
        }
        public IRepository<T> GetRepository<T>() where T : EntityBase
        {
            return new Repository<T>(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
