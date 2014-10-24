using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JPS_Project.Tests.Configuration;
using Domain.Model;
using System.Collections.Generic;
using JPS_Project.Tests.Helpers;

namespace JPS_Project.Tests
{
    [TestClass]
    public class TransactionServiceTest
    {

        GenericConfiguration<TransactionPayment> transactionConfig;
        List<TransactionPayment> transactions;

        [TestInitialize]
        public void TearUp()
        {
            transactionConfig = new GenericConfiguration<TransactionPayment>();
            transactionConfig.Setup();
            transactions = TransactionHelper.GetTranactions(6);
        }


        [TestMethod]
        public void TestMethod1()
        {
            transactionConfig.SetupMockEntityRepositoryForFind(transactions);


        }
    }
}
