﻿using Common.Services;
using Core.Infrastructure;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Concrete
{
    public class TransactionPaymentService : BaseService, ITransactionPaymentService
    {

        public TransactionPaymentService(IPersistenceService persistenceService, ILogService logService)
            : base(persistenceService, logService)
        {

        }
        public IEnumerable<TransactionPayment> GetAllTransactionPayment()
        {
            try
            {
                using (var repository = PersistenceService.GetReadyOnlyRepository<TransactionPayment>())
                {
                    return repository.GetAll().ToList();
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
