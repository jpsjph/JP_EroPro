using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class TransactionSubStatus:EntityBaseFields
    {
        public TransactionSubStatus()
        {
            TransactionStatus = new HashSet<TransactionStatus>();
        }
        public int SubStatusId { get; set; }
        public string SubStatus { get; set; }

        public TransactionPayment TransactionPayment { get; set; }
        public ICollection<TransactionStatus> TransactionStatus { get; set; }
      
     }
}
