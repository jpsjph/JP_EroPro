using Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class TransactionPayment : EntityBaseFields
    {
        public TransactionPayment()
        {
            TransactionStatus = new HashSet<TransactionStatus>();
            TransactionSubStatus = new HashSet<TransactionSubStatus>();
        }
        public int TransactionPaymentId { get; set; }
        public string Reference { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime DateReceived { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal OpeningAmount { get; set; }
        public decimal CurrencyAmount { get; set; }
        public decimal OpeningCurrencyAmount { get; set; }
        public DateTime StatusDate { get; set; }
        public bool IsClosed { get; set; }
        public ICollection<TransactionStatus> TransactionStatus { get; set; }
        public ICollection<TransactionSubStatus> TransactionSubStatus { get; set; }
    }
}
