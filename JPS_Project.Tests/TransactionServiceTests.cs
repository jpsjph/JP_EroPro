using Common.Concrete;
using Common.Services;
using Domain.Model;
using Ero57_Project.Tests.Configuration;
using Ero57_Project.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ero57_Project.Tests
{
    [TestClass]
    public class TransactionServiceTests
    {
        private GenericConfiguration<TransactionPayment> _transConfiguration;
        private List<TransactionPayment> _transList;

        [TestInitialize]
        public void TearUp()
        {
            _transConfiguration = new GenericConfiguration<TransactionPayment>();
            _transConfiguration.Setup();
            _transList = TransactionHelper.GetTransactionList();
            _transactionModel = TransactionHelper.GetTransactionModel();
        }

        [TestMethod]
        public void GetTransactionDetails_Should_Return_Valid_Data()
        {
            const string exceptedTransAccountCode = "12455145442";
            const string exceptedTransactionRef = "110009011496RI";
            _transConfiguration.SetupMockEntityRepositoryForGetAll(_transList);
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            var returnValue = transService.GetTransactionDetails(exceptedTransAccountCode, exceptedTransactionRef);
            _transConfiguration.MockEntity.VerifyAll();
            Assert.IsNotNull(returnValue);
        }

        [TestMethod]
        [Ignore]
        public void GetTransactionDetails_ControllerMethod_Should_Return_CorrectTransaction()
        {
            const string exceptedTransAccountCode = "12455145442";
            const string exceptedTransactionRef = "110009011496RI";
            _transConfiguration.SetupMockEntityRepositoryForGetAll(_transList);
            var controller = new TransactionsController(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            var returnValue = controller.GetTransactionDetails(exceptedTransAccountCode, exceptedTransactionRef, "v1");
            Assert.IsNotNull(returnValue);
            Assert.IsInstanceOfType(returnValue, typeof(OkResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetTransactionDetails_Should_Throw_WhenGiven_Invalid_Params()
        {
            const string invalidTransactionAccountCode = null;
            const string exceptedTransactionRef = "110009011496RI";
            _transConfiguration.SetupMockEntityRepositoryForGetAll(_transList);
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            transService.GetTransactionDetails(invalidTransactionAccountCode, exceptedTransactionRef);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddCreditNoteTransaction_should_Throw_WhenGiven_Invalid_Params()
        {
            _transactionModel.Type = TransType.CRN.ToString();
            _transactionModel.OpeningValue = 1500;
            _transactionModel.Value = 1500;
            _transactionModel.CurrencyValue = -1500;
            _transactionModel.OpeningCurrencyValue = -1500;

            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            transService.AddCreditNoteTransaction(_transactionModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddDebtorNoteTransaction_should_Throw_WhenGiven_Invalid_Params()
        {
            _transactionModel.Type = TransType.CRN.ToString();
            _transactionModel.OpeningValue = 1500;
            _transactionModel.Value = 1500;
            _transactionModel.CurrencyValue = -1500;
            _transactionModel.OpeningCurrencyValue = -1500;

            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            transService.AddDebtorNoteTransaction(_transactionModel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddUnallocatedCashTransactionToAccount_should_Throw_WhenGiven_Invalid_Params()
        {
            _transactionModel.Type = TransType.CRN.ToString();
            _transactionModel.OpeningValue = 1500;
            _transactionModel.Value = 1500;
            _transactionModel.CurrencyValue = -1500;
            _transactionModel.OpeningCurrencyValue = -1500;

            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            transService.AddUnallocatedCashTransactionToAccount(_transactionModel);
        }

        [TestMethod]
        public void AddTransaction_Should_Save_AtLeast_Once()
        {
            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            transService.AddTransaction(_transactionModel);
            _transConfiguration.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());
        }

        [TestMethod]
        public void UpdateTransaction_Should_Update_AtLeast_Once()
        {
            _transactionModel.Id = 2;
            _transactionModel.Closed = false;

            _transConfiguration.SetupMocForPersistence();
            _transConfiguration.MockEntity.Setup(s => s.Find(It.IsAny<Int32>()))
                .Returns(_transList.FirstOrDefault(x => x.ID == _transactionModel.Id));

            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            var result = transService.UpdateTransaction(_transactionModel);
            _transConfiguration.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());
            Assert.AreEqual(result.Billed, _transactionModel.Billed);
        }

        [TestMethod]
        public void AddCreditNoteTransaction_should_Save_AtLeast_Once()
        {
            _transactionModel.Type = TransType.CRN.ToString();
            _transactionModel.OpeningValue = -1500;
            _transactionModel.Value = -1500;
            _transactionModel.CurrencyValue = -1500;
            _transactionModel.OpeningCurrencyValue = -1500;

            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            var result = transService.AddCreditNoteTransaction(_transactionModel);
            _transConfiguration.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());
            Assert.AreEqual(result.Value, _transactionModel.Value);
        }

        [TestMethod]
        public void AddDebtorNoteTransaction_should_Save_AtLeast_Once()
        {
            _transactionModel.Type = TransType.DBN.ToString();
            _transactionModel.OpeningValue = 1500;
            _transactionModel.Value = 1500;
            _transactionModel.CurrencyValue = 1500;
            _transactionModel.OpeningCurrencyValue = 1500;

            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            var result = transService.AddDebtorNoteTransaction(_transactionModel);
            _transConfiguration.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());
            Assert.AreEqual(result.Value, _transactionModel.Value);
        }

        [TestMethod]
        public void AddUnallocatedCashTransactionToAccount_should_Save_AtLeast_Once()
        {
            _transactionModel.Type = TransType.UNC.ToString();
            _transactionModel.OpeningValue = -1500;
            _transactionModel.Value = -1500;
            _transactionModel.CurrencyValue = -1500;
            _transactionModel.OpeningCurrencyValue = -1500;

            _transConfiguration.SetupMocForPersistence();
            var transService = new TransactionService(_transConfiguration.MockPersistence.Object, _transConfiguration.MockLog.Object, _transConfiguration.MockSecurity.Object);
            var result = transService.AddUnallocatedCashTransactionToAccount(_transactionModel);
            _transConfiguration.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());
            Assert.AreEqual(result.Value, _transactionModel.Value);
        }
    }
}