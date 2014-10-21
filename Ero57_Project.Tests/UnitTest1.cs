using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Globalization;
using System.Device.Location;
using NodaTime;
using NodaTime.TimeZones;
using System.Net;
using System.Net.Http;
namespace JPS_Project.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var coordonates = TzdbDateTimeZoneSource.Default.ZoneLocations.Where(x => x.CountryName == "Belgium");
            if (coordonates.Count() > 0)
            {
                var zone = new TimeZoneClient();
                var data = zone.GetTimeZone(coordonates.FirstOrDefault()
                                .Latitude, coordonates.FirstOrDefault()
                                .Longitude, DateTime.UtcNow)
                                .Content.ReadAsAsync<GoogleTimeZone>().Result;
                if (data != null)
                {
                    var raw = DateTime.UtcNow.AddSeconds(data.rawOffset).AddSeconds(data.dstOffset);
                }
            }
        }
    }
}
