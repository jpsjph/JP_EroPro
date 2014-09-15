﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class TransactionSubStatus:EntityBaseFields
    {     
        public int SubStatusId { get; set; }
        public string SubStatus { get; set; }
        public ICollection<TransactionPayment> Transactions { get; set; }      
     }
}
