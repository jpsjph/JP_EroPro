using System;
using System.Collections.Generic;
using System.Linq;
using Common.Concrete;
using Common.Services.ViewModel;
using Core.Infrastructure;
using Domain.Model;
using Ero57_API.Tests.Configuration;
using Ero57_API.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Ero57_API.Tests
{
    [TestClass]
    public class ContactServiceTest
    {
        private GenericConfiguration<Contacts> _contConfiguration;
        private List<Contacts> _contactList;
        private List<Debtor> _debtorList;
        private ContactModel _contactModel;

        [TestInitialize]
        public void TearUp()
        {
            _contConfiguration = new GenericConfiguration<Contacts>();
            _contConfiguration.Setup();
            _contactList = ContactHelper.GetContactLists();
            _debtorList = ContactHelper.GetDebtorList();
            _contactModel = ContactHelper.GetContactModel();
        }

        [TestMethod]
        [Ignore]
        public void GetContactsByType_Should_Return_Valid_Data()
        {
            const string exceptedType = "Accounts";

            var mockDebtor =new Mock<IRepository<Debtor>>();
            var mockPersistence = new Mock<IPersistenceService>();
            mockPersistence.Setup(p => p.GetRepository<Debtor>()).Returns(mockDebtor.Object);
            mockDebtor.Setup(r => r.Get()).Returns(_debtorList.AsQueryable());

            _contConfiguration.SetupMockEntityRepositoryForGetAll(_contactList);
            var contaService = new ContactService(_contConfiguration.MockPersistence.Object, _contConfiguration.MockLog.Object);
            var returnValue = contaService.GetContactsByType(exceptedType);
            _contConfiguration.MockEntity.VerifyAll();
            Assert.IsNotNull(returnValue);
            Assert.AreSame(5, returnValue.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetContactsByType_Should_Throw_WhenGiven_Invalid_Params()
        {
            const string invalidType = null;
            _contConfiguration.SetupMockEntityRepositoryForGetAll(_contactList);
            var contaService = new ContactService(_contConfiguration.MockPersistence.Object, _contConfiguration.MockLog.Object);
            contaService.GetContactsByType(invalidType);
        }

        [TestMethod]
        public void AddContact_Should_Save_AtLeast_Once()
        {
            _contConfiguration.SetupMocForPersistence();
            var contaService = new ContactService(_contConfiguration.MockPersistence.Object, _contConfiguration.MockLog.Object);
            contaService.AddContact(_contactModel);
            _contConfiguration.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());
        }

        [TestMethod]
        public void UpdateContact_Should_Update_AtLeast_Once()
        {
            _contactModel.Id = 5877;
            _contactModel.Mobile = "445457545454";

            _contConfiguration.SetupMocForPersistence();
            _contConfiguration.MockEntity.Setup(s => s.Find(It.IsAny<Int32>()))
                .Returns(_contactList.FirstOrDefault(x => x.ID == _contactModel.Id));

            var contaService = new ContactService(_contConfiguration.MockPersistence.Object, _contConfiguration.MockLog.Object);
            var result = contaService.UpdateContact(_contactModel);
            _contConfiguration.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());
            Assert.AreEqual(result.Mobile, _contactModel.Mobile);
        }
    }
}
