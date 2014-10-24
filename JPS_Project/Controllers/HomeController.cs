using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Model;

namespace JPS_Project.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly IPersistenceService _service;
        public HomeController(IPersistenceService service)
        {
            _service = service;
        }
        public ActionResult Index()
        {
            using (var data=_service.GetRepository<Individual>())
            {
                var d = data.Find(1);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}