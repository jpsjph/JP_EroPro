using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Concrete
{
    public class BaseService
    {
        private readonly IPersistenceService persistenceService;
        private readonly ILogService logervice;
        public BaseService(IPersistenceService persistenceService, ILogService logervice)
        {
            this.persistenceService = persistenceService;
            this.logervice = logervice;
        }

        protected IPersistenceService PersistenceService { get { return persistenceService; } }
        protected ILogService LogService { get { return logervice; } }
    }
}
