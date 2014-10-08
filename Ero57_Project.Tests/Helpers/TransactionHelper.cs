using Common.Services.ViewModel;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ero57_Project.Tests.Helpers
{
    public class TransactionHelper
    {
        public static List<Transaction> GetTransactionList()
        {
            return new List<Transaction>
            {
                new Transaction{ID=1, TransactionRef="110009011496RI", AllocationRef="AllRef01N", DunningProtxPayments=GetDunningProtxPaymentsByTransId(1), Billed=false,BilledDate = DateTime.Now,Client = 1, Client1 =ClientHelper.GetClientById(1), Closed = true,Commission = 15,CurrencyCode = "Euro",Debtor1  = DebtorHelper.GetDebtorById(2) },
                new Transaction{ID=2,TransactionRef="1100090114978RI",AllocationRef="Ref015",DunningProtxPayments=GetDunningProtxPaymentsByTransId(2),Billed=true,BilledDate = DateTime.Now.AddDays(-1),Client = 4,Client1 =ClientHelper.GetClientById(4),Closed = true,Commission = 715,CurrencyCode = "Euro",Debtor1  = DebtorHelper.GetDebtorById(1)},
                new Transaction{ID=4,TransactionRef="110009011496NI",AllocationRef="Ref35",DunningProtxPayments=GetDunningProtxPaymentsByTransId(4),Billed=false,BilledDate = DateTime.Now.AddDays(-15),Client = 3,Client1 =ClientHelper.GetClientById(3),Closed = false,Commission = 155,CurrencyCode = "Euro",Debtor1  = DebtorHelper.GetDebtorById(22)} ,
                new Transaction{ID=15, TransactionRef="1100090154496RI",AllocationRef="Ref8554",DunningProtxPayments=GetDunningProtxPaymentsByTransId(15),Billed=true,BilledDate = DateTime.Now.AddDays(5),Client = 31,Client1 =ClientHelper.GetClientById(31),Closed = false,Commission = 175,CurrencyCode = "Euro",Debtor1  = DebtorHelper.GetDebtorById(112)}
            };
        }

        public static List<Debtor> GetDebtorList()
        {
            return new List<Debtor>
            {
                new Debtor {ID = 2, AccountCode = "12455145442", Address1 = "Namur"},
                new Debtor {ID = 1, AccountCode = "124578987005", Address1 = "BXL"},
                new Debtor {ID = 22, AccountCode = "1245514585454", Address1 = "London"},
                new Debtor {ID = 12, AccountCode = "12455144415", Address1 = "Tokyo"},
                new Debtor {ID = 112, AccountCode = "12455112145", Address1 = "NY"}
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

        public static List<TransactionType> GetTransactionTypeList()
        {
            return new List<TransactionType>
            {
                new TransactionType{ID ="ADC",TransactionType1 = "Accounting Document",Description ="CHEP Accounting Document",DisplayOrder = 11,LicenceID = 1},
                new TransactionType{ID ="DIS",TransactionType1 = "Discount",Description ="Discount",DisplayOrder = 10,LicenceID = 1},
                new TransactionType{ID ="PAY",TransactionType1 = "Payment",Description ="Payments",DisplayOrder = 15,LicenceID = 1}
            };
        }

        public static BaseTransactionModel GetTransBaseModel()
        {
            return new BaseTransactionModel { AccountCode = "12455144415", CurrencyCode = "EUR", CurrencyValue = 1500, Date = DateTime.Now, Description = "Description", LedgerIdentifier = "Test1", Value = 1500, TransactionRef = "NN001101REF" };
        }

        public static TransactionPaymentModel GetTransactionPaymentModel()
        {
            return new TransactionPaymentModel { AccountCode = "12455144415", CurrencyCode = "EUR", CurrencyValue = 1500, Date = DateTime.Now, Description = "Description", LedgerIdentifier = "Test1", Value = 1500, TransactionRef = "NN001101REF", IsEroCCPayment = true, ChildTransactions = GetChildTransactions(), CreditCardPaymentRef = "A-576087-A-0000001" };
        }

        public static List<DunningProtxPayments> GetDunningProtxPaymentsByTransId(int transId)
        {
            var listDunning = new List<DunningProtxPayments>{
                new DunningProtxPayments{TransactionID=1,CCTranID="A-576087-A-0000001",TransactionAmount=450, ProtxPayments=GetProtxPaymentsByCCTranId("A-576087-A-0000001") },
                new DunningProtxPayments{TransactionID=1,CCTranID="A-224060-A-0000002",TransactionAmount=450, ProtxPayments=GetProtxPaymentsByCCTranId("A-224060-A-0000002")},
                new DunningProtxPayments{TransactionID=2,CCTranID="A-224060-A-0000003",TransactionAmount=450, ProtxPayments=GetProtxPaymentsByCCTranId("A-224060-A-0000003")},
                new DunningProtxPayments{TransactionID=3,CCTranID="A-224060-A-0000004",TransactionAmount=450, ProtxPayments=GetProtxPaymentsByCCTranId("A-224060-A-0000004")},
                new DunningProtxPayments{TransactionID=4,CCTranID="A-224060-A-00000025",TransactionAmount=450, ProtxPayments=GetProtxPaymentsByCCTranId("A-224060-A-00000025")}
            };
            return listDunning.Where(x => x.TransactionID == transId).ToList();
        }

        public static ProtxPayments GetProtxPaymentsByCCTranId(string ccTranId)
        {
            var listProtx = new List<ProtxPayments>{
                new ProtxPayments{CCTranID="A-576087-A-0000001", Description="106985", subtotal=7539.81M, total=7539.81M},
                new ProtxPayments{CCTranID="A-224060-A-0000002", Description="106985", subtotal=7539.81M, total=7539.81M},
                new ProtxPayments{CCTranID="A-224060-A-0000003", Description="106985", subtotal=7539.81M, total=7539.81M},
                new ProtxPayments{CCTranID="A-224060-A-0000004", Description="106985", subtotal=7539.81M, total=7539.81M}
            };
            return listProtx.FirstOrDefault(x => x.CCTranID == ccTranId);
        }

        private static List<ChildTransaction> GetChildTransactions()
        {
            return new List<ChildTransaction>{
                new ChildTransaction{TransactionRef="1100090154496RI", AccountCode="12455144415", LedgerIdentifier="Test1", PaymentAmount=500},
                new ChildTransaction{TransactionRef="1100090114978RI", AccountCode="12455144415", LedgerIdentifier="Test1", PaymentAmount=5000},
                new ChildTransaction{TransactionRef="110009011496NI", AccountCode="12455144415", LedgerIdentifier="Test1", PaymentAmount=50000},
                new ChildTransaction{TransactionRef="1100090114978RI", AccountCode="12455144415", LedgerIdentifier="Test1", PaymentAmount=500000},
                new ChildTransaction{TransactionRef="1100090114978RI", AccountCode="12455144415", LedgerIdentifier="Test1", PaymentAmount=50000000}
            };
        }

        public static CreditTransactionModel GetCreditTransactionModel()
        {
            return new CreditTransactionModel { AccountCode = "12455144415", CurrencyCode = "EUR", CurrencyValue = 1500, Date = DateTime.Now, Description = "Description", LedgerIdentifier = "Test1", Value = 1500, TransactionRef = "NN001101REF", ChildTransactions = GetChildTransactions() };
        }
    }
}