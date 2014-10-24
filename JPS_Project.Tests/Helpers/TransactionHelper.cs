using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPS_Project.Tests.Helpers
{
    public class TransactionHelper
    {
        public static List<TransactionPayment> GetTranactions(int length)
        {
            var result = new List<TransactionPayment>();
            for (int i = 0; i < length; i++)
            {
                var trans = new TransactionPayment { TransactionPaymentId=i,Amount=(500+i), CurrencyAmount=(1000+i), InvoiceNumber="INV01121"+i.ToString()
                    ,Reference="REf000"+i.ToString(),StatusDate=DateTime.Now.AddDays(2), DueDate=DateTime.Now.AddDays(10),IsClosed=false,CreatorId=i, DateCreated=DateTime.Now};

                result.Add(trans);                
            }
            return result;
        }
    }
}
