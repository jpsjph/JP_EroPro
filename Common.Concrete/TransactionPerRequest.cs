using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Services.Tasks;
using Core.Infrastructure;
using System.Web;

namespace Common.Concrete
{
    public class TransactionPerRequest:IRunOnRequest,IRunOnError,IRunAfterRequest
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
            throw new NotImplementedException();
        }

        void IRunOnError.Execute()
        {
            throw new NotImplementedException();
        }

        void IRunAfterRequest.Execute()
        {
            throw new NotImplementedException();
        }
    }
}
