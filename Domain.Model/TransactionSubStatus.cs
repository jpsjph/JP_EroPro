using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class TransactionSubStatus
    {
        public int SubStatusId { get; set; }
        public string SubStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int CreatorId { get; set; }
        public int UpdatorId { get; set; }
    }
}
