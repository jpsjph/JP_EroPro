using Common.Concrete.Helpers;
using Common.Services.ViewModel;
using Core.Infrastructure;
using Domain.Model;
using Ero57_API.Tests.Configuration;
using Ero57_API.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ero57_API.Tests
{
    [TestClass]
    public class TransactionRuleTest
    {
        private GenericConfiguration<Transaction> _transConfiguration;
        private List<Transaction> _transList;
        private List<Debtor> _debtorList;
        private TransactionPaymentModel _transactionModel;
        private CreditTransactionModel _creditModel;
        private UnallocatedTransactionModel _unallocateModel;
        private BaseTransactionModel baseModel;
        private Dictionary<string, string> message;
        private Mock<IRepository<Debtor>> mockDebtor;

        [TestInitialize]
        public void TearUp()
        {
            _transConfiguration = new GenericConfiguration<Transaction>();
            _transConfiguration.Setup();
            _transList = TransactionHelper.GetTransactionList();
            baseModel = TransactionHelper.GetTransBaseModel();
            _debtorList = DebtorHelper.GetDebtorList();
            _transactionModel = TransactionHelper.GetTransactionPaymentModel();
            _creditModel = TransactionHelper.GetCreditTransactionModel();
            message = new Dictionary<string, string>();
            mockDebtor = new Mock<IRepository<Debtor>>();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IsParentTransactionReferenceNotExists_Should_Throw()
        {
            baseModel = null;
            var mockTrans = new Mock<IRepository<Transaction>>();
            var trans = new TransactionRules(mockTrans.Object, mockDebtor.Object);
            trans.IsParentTransactionReferenceNotExists(baseModel, out message);
        }

        [TestMethod]
        public void IsParentTransactionReferenceNotExists_IsTrue_WhenGivenValidData()
        {
            _transConfiguration.SetupMockEntityRepositoryForGetAll(_transList);
            var trans = new TransactionRules(_transConfiguration.MockEntity.Object, mockDebtor.Object);
            Assert.IsTrue(trans.IsParentTransactionReferenceNotExists(baseModel, out message));
        }

        [TestMethod]
        public void IsParentTransactionReferenceNotExists_IsFalse_WhenTransactionExist()
        {
            baseModel.TransactionRef = "110009011496NI";
            _transConfiguration.SetupMockEntityRepositoryForGetAll(_transList);
            var trans = new TransactionRules(_transConfiguration.MockEntity.Object, mockDebtor.Object);
            Assert.IsFalse(trans.IsParentTransactionReferenceNotExists(baseModel, out message));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IsUniqueDebtor_shouldThrow_whenGivenNullArgument()
        {
            baseModel = null;
            var mockTrans = new Mock<IRepository<Transaction>>();
            var trans = new TransactionRules(mockTrans.Object, mockDebtor.Object);
            trans.IsUniqueDebtor(baseModel, out message);
        }

        [TestMethod]
        public void IsUniqueDebtor_IsTrue_WhenGivenValidData()
        {
            mockDebtor.Setup(x => x.Get()).Returns(_debtorList.AsQueryable());
            var trans = new TransactionRules(_transConfiguration.MockEntity.Object, mockDebtor.Object);
            Assert.IsTrue(trans.IsUniqueDebtor(baseModel, out message));
        }

        [TestMethod]
        public void IsUniqueDebtor_IsFalse_WhenGivenInvalidData()
        {
            baseModel.AccountCode = "NNG0075";
            mockDebtor.Setup(x => x.Get()).Returns(_debtorList.AsQueryable());
            var trans = new TransactionRules(_transConfiguration.MockEntity.Object, mockDebtor.Object);
            Assert.IsFalse(trans.IsUniqueDebtor(baseModel, out message));
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IsCreditCardPaymentReferenceExists_shouldThrow_whenGivenNullArgument()
        {
            _transactionModel = null;
            var mockTrans = new Mock<IRepository<Transaction>>();
            var trans = new TransactionRules(mockTrans.Object, mockDebtor.Object);
            trans.IsCreditCardPaymentReferenceExists(_transactionModel, out message);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void IsCreditCardPaymentReferenceExists_shouldThrow_whenCreditCardPaymentRefIsNull()
        {
            _transactionModel.CreditCardPaymentRef = null;
            var mockTrans = new Mock<IRepository<Transaction>>();
            var trans = new TransactionRules(mockTrans.Object, mockDebtor.Object);
            trans.IsCreditCardPaymentReferenceExists(_transactionModel, out message);
        }

        [TestMethod]
        public void IsCreditCardPaymentReferenceExists_IsTrue_WhenGivenValidData()
        {
            var message = new Dictionary<string, string>();
            var mockDebtor = new Mock<IRepository<Debtor>>();
            _transConfiguration.SetupMockEntityRepositoryForGetAll(_transList);
            var trans = new TransactionRules(_transConfiguration.MockEntity.Object, mockDebtor.Object);
            Assert.IsTrue(trans.IsCreditCardPaymentReferenceExists(_transactionModel, out message));
        }

        [TestMethod]
        public void IsCreditCardPaymentReferenceExists_IsFalse_WhenGivenValidData()
        {
            _transactionModel.CreditCardPaymentRef = "CC001REF";
            _transConfiguration.SetupMockEntityRepositoryForGetAll(_transList);
            var trans = new TransactionRules(_transConfiguration.MockEntity.Object, mockDebtor.Object);
            Assert.IsFalse(trans.IsCreditCardPaymentReferenceExists(_transactionModel, out message));
        }

        [TestMethod]
        public void IsChildCreditTransactionExists_IsTrue_WhenGivenValidData()
        {
            _transConfiguration.SetupMockEntityRepositoryForGetAll(_transList);
            var trans = new TransactionRules(_transConfiguration.MockEntity.Object, mockDebtor.Object);
            Assert.IsTrue(trans.IsChildTransactionExists(null, _creditModel, false, out message));
        }

        [TestMethod]
        public void IsChildCreditTransactionExists_IsFalse_WhenGivenInvalidData()
        {
            _creditModel.LedgerIdentifier = "Test";
            _transConfiguration.SetupMockEntityRepositoryForGetAll(_transList);
            var trans = new TransactionRules(_transConfiguration.MockEntity.Object, mockDebtor.Object);
            Assert.IsFalse(trans.IsChildTransactionExists(null, _creditModel, false, out message));
        }

        [TestMethod]
        public void IsChildCreditTransactionExists_IsFalse_WhenChildListIsEmpy()
        {
            _creditModel.ChildTransactions = new List<ChildTransaction>();
            _transConfiguration.SetupMockEntityRepositoryForGetAll(_transList);
            var trans = new TransactionRules(_transConfiguration.MockEntity.Object, mockDebtor.Object);
            Assert.IsFalse(trans.IsChildTransactionExists(null, _creditModel, false, out message));
        }
    }
}