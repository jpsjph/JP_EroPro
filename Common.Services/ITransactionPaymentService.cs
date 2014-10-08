using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public interface ITransactionPaymentService
    {
        /// <summary>
        /// Get all transaction payments
        /// </summary>
        /// <returns>List of TransactionPayment</returns>
        IEnumerable<TransactionPayment>GetAllTransactionPayment();

    }
}
