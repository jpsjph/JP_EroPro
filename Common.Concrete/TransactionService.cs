using Common.Services;
using Core.Infrastructure;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Concrete
{
    public class TransactionService : BaseService, ITransactionService
    {

        public TransactionService(IPersistenceService persistenceService, ILogService logService)
            : base(persistenceService, logService)
        {

        }
        public IQueryable<TransactionPayment> GetAllTransactionPayment()
        {
            try
            {
                using (var repository = PersistenceService.GetReadyOnlyRepository<TransactionPayment>())
                {
                    return repository.GetAll();
                }
            }
            catch (Exception ex)
            {
                LogService.Error("GetAllTransactionPayment error", ex);
                throw;
            }
        }
    }
}
