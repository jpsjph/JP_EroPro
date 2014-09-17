using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ero57_Project.Controllers
{
    public class TransactionController : Controller
    {
        
        public TransactionController(IPersistenceService service, ILogService logService)
        {

        }
        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }
    }
}