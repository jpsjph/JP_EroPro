﻿using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Address : EntityBaseFields
    {
        public Address()
        {
            Individuals = new HashSet<Individual>();
        }
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
     
        public ClientAddress ClientAddress { get; set; }
        public virtual ICollection<Individual> Individuals { get; set; }
    }
}
