using Core.Infrastructure;
using Domain.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Client : EntityBaseFields
    {
        public int ClientId { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public int BaseCountry { get; set; }
        public Status ClientStatus { get; set; }
        public ClientType ClientType { get; set; }
        public string TradingName { get; set; }        
        public ClientAddress Address { get; set; }     
    }
}
