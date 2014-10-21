using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;


namespace JPS_Project.Tests
{
    public class TimeZoneClient
    {
        public HttpResponseMessage GetTimeZone(double latitude, double longitude, DateTime utcDate)
        {
            string _endPoint;
            using (var client = new HttpClient())
            {
                _endPoint = "https://maps.googleapis.com/maps/api/timezone/json";
                var parametters = new List<string>();
                parametters.Add(string.Format("?location={0},{1}", latitude, longitude));
                parametters.Add(string.Concat("timestamp=", ToTimeStamp(utcDate)));

                _endPoint += string.Join("&", parametters);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return client.GetAsync(_endPoint).Result;
            }
        }

        public long ToTimeStamp(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerSecond;
            return unixTimestamp;
        }

    }
}
