using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JPS_Project.Alerts
{
    public class AlertActionResult:ActionResult
    {
        public ActionResult InnerResult { get; set; }
		public string AlertClass { get; set; }
		public string Message { get; set; }

        public AlertActionResult(ActionResult innerResult, string alertClass, string message)
		{
			InnerResult = innerResult;
			AlertClass = alertClass;
			Message = message;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			var alerts = context.Controller.TempData.GetAlerts();
			alerts.Add(new Alerts(AlertClass, Message));
			InnerResult.ExecuteResult(context);
		}
    }
}