using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JPS_Project.Tests
{
    public class GoogleTimeZone
    {
        public double dstOffset { get; set; }
        public double rawOffset { get; set; }
        public string status { get; set; }
        public string timeZoneId { get; set; }
        public string timeZoneName { get; set; }
    }
}
