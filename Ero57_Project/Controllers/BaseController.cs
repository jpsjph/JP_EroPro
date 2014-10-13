using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ero57_Project.Controllers
{
    public class BaseController : Controller
    {

        private readonly ILogService _logService;
        public BaseController(ILogService logService)
        {
            _logService = logService;
        }

        protected ILogService LogService { get { return _logService; } }
    }
}