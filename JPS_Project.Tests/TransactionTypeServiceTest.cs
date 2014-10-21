using System;
using System.Collections.Generic;
using Common.Concrete;
using Domain.Model;
using Ero57_Project.Tests.Configuration;
using Ero57_Project.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ero57_Project.Tests
{
    [TestClass]
    public class TransactionTypeServiceTest
    {
        private GenericConfiguration<TransactionType> _transTypeConfiguration;
        private List<TransactionType> _transTypeList;

        [TestInitialize]
        public void TearUp()
        {
            _transTypeConfiguration = new GenericConfiguration<TransactionType>();
            _transTypeConfiguration.Setup();
            _transTypeList = TransactionHelper.GetTransactionTypeList();
        }

        [TestMethod]
        public void GetTransactionTypeByCode_Should_Return_Valid_Data()
        {
            const string exceptedCode = "PAY";
            const int actualValue = 1;

            _transTypeConfiguration.SetupMockEntityRepositoryForGetAll(_transTypeList);
            var transService = new TransactionService(_transTypeConfiguration.MockPersistence.Object, _transTypeConfiguration.MockLog.Object, _transTypeConfiguration.MockSecurity.Object);
            var returnValue = transService.GetTransactionTypeByCode(exceptedCode);
            _transTypeConfiguration.MockEntity.VerifyAll();

            Assert.IsNotNull(returnValue);
            Assert.AreEqual(returnValue.LicenceID, actualValue);
        }

        [TestMethod]
        public void GetTransactionTypeByCode_Should_Return_Null_WhenGiven_NotExistsCode()
        {
            const string exceptedcode = "NotExistsCode";

            _transTypeConfiguration.SetupMockEntityRepositoryForGetAll(_transTypeList);
            var transService = new TransactionService(_transTypeConfiguration.MockPersistence.Object, _transTypeConfiguration.MockLog.Object, _transTypeConfiguration.MockSecurity.Object);
            var returnValue = transService.GetTransactionTypeByCode(exceptedcode);
            _transTypeConfiguration.MockEntity.VerifyAll();
            Assert.IsNull(returnValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetTransactionTypeByCode_Should_Throw_WhenGiven_Invalid_Params()
        {
            const string invalidCode = null;
            _transTypeConfiguration.SetupMockEntityRepositoryForGetAll(_transTypeList);
            var transService = new TransactionService(_transTypeConfiguration.MockPersistence.Object, _transTypeConfiguration.MockLog.Object, _transTypeConfiguration.MockSecurity.Object);
            transService.GetTransactionTypeByCode(invalidCode);
        }


        [TestMethod]
        public void GetTransactionTypeByDescription_Should_Return_Valid_Data()
        {
            const string exceptedDescription = "Discount";
            const int actualValue = 10;

            _transTypeConfiguration.SetupMockEntityRepositoryForGetAll(_transTypeList);
            var transService = new TransactionService(_transTypeConfiguration.MockPersistence.Object, _transTypeConfiguration.MockLog.Object, _transTypeConfiguration.MockSecurity.Object);
            var returnValue = transService.GetTransactionTypeByDescription(exceptedDescription);
            _transTypeConfiguration.MockEntity.VerifyAll();

            Assert.IsNotNull(returnValue);
            Assert.AreEqual(returnValue.DisplayOrder, actualValue);
        }

        [TestMethod]
        public void GetTransactionTypeByDescription_Should_Return_Null_WhenGiven_NotExistsDescription()
        {
            const string exceptedDescription = "NotExistsDescrip";

            _transTypeConfiguration.SetupMockEntityRepositoryForGetAll(_transTypeList);
            var transService = new TransactionService(_transTypeConfiguration.MockPersistence.Object, _transTypeConfiguration.MockLog.Object, _transTypeConfiguration.MockSecurity.Object);
            var returnValue = transService.GetTransactionTypeByDescription(exceptedDescription);
            _transTypeConfiguration.MockEntity.VerifyAll();
            Assert.IsNull(returnValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetTransactionTypeByDescription_Should_Throw_WhenGiven_Invalid_Params()
        {
            const string invalidDescription = null;
            _transTypeConfiguration.SetupMockEntityRepositoryForGetAll(_transTypeList);
            var transService = new TransactionService(_transTypeConfiguration.MockPersistence.Object, _transTypeConfiguration.MockLog.Object, _transTypeConfiguration.MockSecurity.Object);
            transService.GetTransactionTypeByDescription(invalidDescription);
        }
    }
}
