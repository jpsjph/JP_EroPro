using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JPS_Project.Alerts
{
    public class Alerts
    {
        public string AlertClass { get; set; }
        public string Message { get; set; }
        public Alerts(string alertClass, string message)
        {
            AlertClass = alertClass;
            Message = message;
        }
    }
}