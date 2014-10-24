using Common.Services;
using Core.Infrastructure;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using JPS_Project.Models;
namespace JPS_Project.Controllers
{
    public class TransactionController : Controller
    {

        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetTransaction([DataSourceRequest] DataSourceRequest request)
        {
            var result = _transactionService.GetAllTransactionPayment().Project().To<TransactionModel>();
            return Json(result.ToList().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}