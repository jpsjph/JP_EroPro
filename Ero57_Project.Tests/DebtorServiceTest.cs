using Common.Concrete;
using Common.Services.ViewModel;
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
    public class DebtorServiceTest
    {
        private GenericConfiguration<Debtor> _debtorConfiguration;
        private List<Debtor> _debtorList;
        private DebtorModel _debtorModel;

        [TestInitialize]
        public void TearUp()
        {
            _debtorConfiguration = new GenericConfiguration<Debtor>();
            _debtorConfiguration.Setup();
            _debtorList = DebtorHelper.GetDebtorList();
            _debtorModel = DebtorHelper.GetDebtorModel();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDebtorDetailsByAccCode_Should_Throw_WhenGiven_Invalid_Params()
        {
            const string invalidType = null;
            _debtorConfiguration.SetupMockEntityRepositoryForGetAll(_debtorList);
            var contaService = new DebtorService(_debtorConfiguration.MockPersistence.Object, _debtorConfiguration.MockLog.Object, _debtorConfiguration.MockSecurity.Object);
            contaService.GetDebtorDetailsByAccCode(invalidType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDebtorDetails_Should_Throw_WhenGiven_Invalid_Params()
        {
            const string invalidType = null;
            const string invaliddunsNumber = null;
            const string invalidcompReg = null;
            _debtorConfiguration.SetupMockEntityRepositoryForGetAll(_debtorList);
            var contaService = new DebtorService(_debtorConfiguration.MockPersistence.Object, _debtorConfiguration.MockLog.Object, _debtorConfiguration.MockSecurity.Object);
            contaService.GetDebtorDetails(invalidType, invaliddunsNumber, invalidcompReg);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDebtorse_Should_Throw_WhenGiven_Invalid_Params()
        {
            const string invalidName = null;
            _debtorConfiguration.SetupMockEntityRepositoryForGetAll(_debtorList);
            var contaService = new DebtorService(_debtorConfiguration.MockPersistence.Object, _debtorConfiguration.MockLog.Object, _debtorConfiguration.MockSecurity.Object);
            contaService.GetDebtors(invalidName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDebtorChaseNotesByAccountCode_Should_Throw_WhenGiven_Invalid_Params()
        {
            const string invalidCode = null;
            var invalidDateFrom = new DateTime();
            var invalidDateTo = new DateTime();
            _debtorConfiguration.SetupMockEntityRepositoryForGetAll(_debtorList);
            var contaService = new DebtorService(_debtorConfiguration.MockPersistence.Object, _debtorConfiguration.MockLog.Object, _debtorConfiguration.MockSecurity.Object);
            contaService.GetDebtorChaseNotesByAccountCode(invalidCode);
            contaService.GetDebtorChaseNotesByAccountCode(invalidCode, invalidDateFrom, invalidDateTo);
        }

        [TestMethod]
        public void GetDebtorDetailsByAccCode_Should_Return_Valid_Data()
        {
            const string validAccountCode = "12455144415";
            const string exceptedValue = "Tkyo";

            _debtorConfiguration.SetupMockEntityRepositoryForGetAll(_debtorList);
            var transService = new DebtorService(_debtorConfiguration.MockPersistence.Object, _debtorConfiguration.MockLog.Object, _debtorConfiguration.MockSecurity.Object);
            var returnValue = transService.GetDebtorDetailsByAccCode(validAccountCode);
            _debtorConfiguration.MockEntity.VerifyAll();

            Assert.IsNotNull(returnValue);
            Assert.IsNotNull(returnValue.FirstOrDefault());
            Assert.AreEqual(returnValue.FirstOrDefault().Address1, exceptedValue);
        }

        [TestMethod]
        public void GetDebtorDetails_Should_Return_Valid_Data()
        {
            const string validAccountCode = "12455144415";
            const string validDunsNumber = "5154";
            const string validCompReg = "1739";
            const string exceptedValue = "Tkyo";

            _debtorConfiguration.SetupMockEntityRepositoryForGetAll(_debtorList);
            var transService = new DebtorService(_debtorConfiguration.MockPersistence.Object, _debtorConfiguration.MockLog.Object, _debtorConfiguration.MockSecurity.Object);
            var returnValue = transService.GetDebtorDetails(validAccountCode, validDunsNumber, validCompReg);
            _debtorConfiguration.MockEntity.VerifyAll();

            Assert.IsNotNull(returnValue);
            Assert.AreEqual(returnValue.FirstOrDefault().Address1, exceptedValue);
        }

        [TestMethod]
        public void GetDebtorDetails_Should_Return_Nothing()
        {
            const string invalidAccountCode = "12455144455";
            const string invalidDunsNumber = "5654";
            const string invalidCompReg = "1739";

            _debtorConfiguration.SetupMockEntityRepositoryForGetAll(_debtorList);
            var transService = new DebtorService(_debtorConfiguration.MockPersistence.Object, _debtorConfiguration.MockLog.Object, _debtorConfiguration.MockSecurity.Object);
            var returnValue = transService.GetDebtorDetails(invalidAccountCode, invalidDunsNumber, invalidCompReg);
            _debtorConfiguration.MockEntity.VerifyAll();
            Assert.IsNotNull(returnValue);
            Assert.IsTrue(returnValue.Count == 0);
        }

        [TestMethod]
        public void GetDebtorDetailsByAccCode_Should_Return_Nothing()
        {
            const string invalidAccountCode = "124551445415";

            _debtorConfiguration.SetupMockEntityRepositoryForGetAll(_debtorList);
            var transService = new DebtorService(_debtorConfiguration.MockPersistence.Object, _debtorConfiguration.MockLog.Object, _debtorConfiguration.MockSecurity.Object);
            var returnValue = transService.GetDebtorDetailsByAccCode(invalidAccountCode);
            _debtorConfiguration.MockEntity.VerifyAll();

            Assert.IsNotNull(returnValue);
            Assert.IsTrue(returnValue.Count == 0);
        }

        [TestMethod]
        public void AddDebtor_Should_Save_AtLeast_Once()
        {
            _debtorConfiguration.SetupMocForPersistence();
            var transService = new DebtorService(_debtorConfiguration.MockPersistence.Object, _debtorConfiguration.MockLog.Object, _debtorConfiguration.MockSecurity.Object);
            transService.AddDebtor(_debtorModel);
            _debtorConfiguration.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());
        }
    }
}