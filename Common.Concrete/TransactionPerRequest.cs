using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Services.Tasks;
using Core.Infrastructure;
using System.Web;
using System.Data.Entity;

namespace Common.Concrete
{
    public class TransactionPerRequest : IRunOnRequest, IRunOnError, IRunAfterRequest
    {
        private readonly IDataContext _context;
        private readonly HttpContextBase _httpContext;
        public TransactionPerRequest(IDataContext context, HttpContextBase httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }
        void IRunOnRequest.Execute()
        {
            _httpContext.Items["_Transaction"] =
                _context.DatabaseContext.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
        }

        void IRunOnError.Execute()
        {
            _httpContext.Items["_Error"] = true;
        }

        void IRunAfterRequest.Execute()
        {
            var transaction = (DbContextTransaction)_httpContext.Items["_Transaction"];

            if (_httpContext.Items["_Error"] != null)
                transaction.Rollback();
            else
                transaction.Commit();

        }
    }
}
