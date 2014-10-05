using Common.Services.ViewModel;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ero57_API.Tests.Helpers
{
    public static class DebtorHelper
    {
        public static List<Debtor> GetDebtorList()
        {
            return new List<Debtor>
            {
                new Debtor {ID = 2, AccountCode = "12455145442", Address1 = "Namur", CompanyName = "FLUOR LTD", Client = 3,Client1=ClientHelper.GetClientById(3), CreditLimit = 5000,DebtType = 1,DunsNumber = "154",CompanyReg ="179"},
                new Debtor {ID = 1, AccountCode = "124578987005", Address1 = "BXL", CompanyName = "MSI", Client = 1,Client1=ClientHelper.GetClientById(1),CreditLimit = 600,DebtType = 1,DunsNumber = "1504",CompanyReg ="1709"},
                new Debtor {ID = 22, AccountCode = "1245514585454", Address1 = "London", CompanyName = "FSI LTD", Client = 2,Client1=ClientHelper.GetClientById(2),CreditLimit = 7000,DebtType = 1,DunsNumber = "15004",CompanyReg ="1079"},
                new Debtor {ID = 12, AccountCode = "12455144415", Address1 = "Tkyo", CompanyName = "SLM LTD", Client = 3,Client1=ClientHelper.GetClientById(3),CreditLimit = 501000,DebtType = 1,DunsNumber = "5154",CompanyReg ="1739"},
                new Debtor {ID = 112, AccountCode = "12455112145", Address1 = "NY", CompanyName = "CORA LTD", Client = 1,Client1=ClientHelper.GetClientById(1),CreditLimit = 50000,DebtType = 1,DunsNumber = "2500",CompanyReg ="1379"}
            };
        }

        public static TransactionModel GetTransactionModel()
        {
            return new TransactionModel
            {
                TransactionRef = "1110001REF",
                AllocationRef = "AllRef01N",
                Billed = false,
                BilledDate = DateTime.Now,
                Client = 1,
                Closed = true,
                Commission = 15,
                CurrencyCode = "Euro",
                Debtor = 2
            };
        }

        public static DebtorModel GetDebtorModel()
        {
            return new DebtorModel
            {
                Id = 0,
                AccountCode = "12455145442",
                Address1 = "Namur",
                CompanyName = "FLUOR LTD",
                Client = 3,
                DebtType = 1
            };
        }

        public static Debtor GetDebtorById(int id)
        {
            return GetDebtorList().FirstOrDefault(x => x.ID == id);
        }
    }
}