using Common.Services.Map;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JPS_Project.Models
{
    public class AddressModel : IMapFrom<Address>
    {
        public int AddressId { get; set; }
        public string Building { get; set; }
        public string Number { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
    }
}