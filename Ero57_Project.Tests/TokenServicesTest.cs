using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ero57_Project.Tests.Configuration;
using Domain.Model;
using System.Collections.Generic;
using Ero57_Project.Tests.Helpers;
using Common.Concrete;
using Core.Infrastructure;
using System.Linq;
using Moq;

namespace Ero57_Project.Tests
{
    [TestClass]
    public class TokenServicesTest
    {
        private GenericConfiguration<AuthToken> _authTokenConfig;
        private GenericConfiguration<ClientLogin> _clientLogin;
        private List<AuthToken> _authTokenList;
        private List<ClientLogin> _clientLoginList;
        private TokenModel _tokenModel;
        private AuthToken _authToken;

        [TestInitialize]
        public void TearUp()
        {
            _authTokenConfig = new GenericConfiguration<AuthToken>();
            _clientLogin = new GenericConfiguration<ClientLogin>();
            _authTokenConfig.Setup();
            //_clientLogin.Setup();
            _authTokenList = TokenHelper.GetTokenModelList();
            _clientLoginList = TokenHelper.GetClientLoginList();
            _tokenModel = TokenHelper.GetModel();
            _authToken = TokenHelper.GetAuthTokenModel();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateToken_Should_Throw_When_Pass_InvalidParam()
        {
            //AAA
            TokenModel exceptedValue = null;
            _authTokenConfig.SetupMockEntityRepositoryForGetAll(_authTokenList);
            var service = new SecurityService(_authTokenConfig.MockPersistence.Object, _authTokenConfig.MockLog.Object, _authTokenConfig.MockUtility.Object);
            service.GenerateToken(exceptedValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetToken_Should_Throw_When_Pass_InvalidParam()
        {
            //AAA
            const int exceptedValue = -1;
            _authTokenConfig.SetupMockEntityRepositoryForGetAll(_authTokenList);
            var service = new SecurityService(_authTokenConfig.MockPersistence.Object, _authTokenConfig.MockLog.Object, _authTokenConfig.MockUtility.Object);
            service.GetToken(exceptedValue);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetToken_With_Username_Should_Throw_When_Pass_InvalidParam()
        {
            //AAA
            string exceptedUsername = null;
            const string exceptedPassword = "";
            _authTokenConfig.SetupMockEntityRepositoryForGetAll(_authTokenList);
            var service = new SecurityService(_authTokenConfig.MockPersistence.Object, _authTokenConfig.MockLog.Object, _authTokenConfig.MockUtility.Object);
            service.GetToken(exceptedUsername, exceptedPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsValid_With_Username_Should_Throw_When_Pass_InvalidParam()
        {
            //AAA
            string exceptedUsername = null;
            const string exceptedPassword = "";
            _authTokenConfig.SetupMockEntityRepositoryForGetAll(_authTokenList);
            var service = new SecurityService(_authTokenConfig.MockPersistence.Object, _authTokenConfig.MockLog.Object, _authTokenConfig.MockUtility.Object);
            service.IsValid(exceptedUsername, exceptedPassword);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DecryptUsername_Should_Throw_When_Pass_InvalidParam()
        {
            //AAA
            string exceptedkey = null;

            _authTokenConfig.SetupMockEntityRepositoryForGetAll(_authTokenList);
            var service = new SecurityService(_authTokenConfig.MockPersistence.Object, _authTokenConfig.MockLog.Object, _authTokenConfig.MockUtility.Object);
            service.DecryptUsername(exceptedkey);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GenerateToken_With_Username_Should_Throw_When_Pass_InvalidParam()
        {
            //AAA
            string exceptedUsername = null;
            const string exceptedPassword = "";

            _authTokenConfig.SetupMockEntityRepositoryForGetAll(_authTokenList);
            var service = new SecurityService(_authTokenConfig.MockPersistence.Object, _authTokenConfig.MockLog.Object, _authTokenConfig.MockUtility.Object);
            service.GenerateToken(exceptedUsername, exceptedPassword);
        }

        [TestMethod]
        public void GetToken_Should_Pass_When_Given_Valid_Data()
        {
            //AAA
            const int validKey = 1;
            const string exceptedData = "testtOKEN1";
            _authTokenConfig.SetupMockEntityRepositoryForGetAll(_authTokenList);
            var service = new SecurityService(_authTokenConfig.MockPersistence.Object, _authTokenConfig.MockLog.Object, _authTokenConfig.MockUtility.Object);
            var result = service.GetToken(validKey);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Token, exceptedData);
        }

        [TestMethod]
        public void GetToken_Should_Return_Null_When_Given_Invalid_Data()
        {
            //AAA
            const int validKey = 11;
            _authTokenConfig.SetupMockEntityRepositoryForGetAll(_authTokenList);
            var service = new SecurityService(_authTokenConfig.MockPersistence.Object, _authTokenConfig.MockLog.Object, _authTokenConfig.MockUtility.Object);
            var result = service.GetToken(validKey);
            Assert.IsNull(result);
        }

        [TestMethod]
        [Ignore]
        public void GenerateToken_Should_Pass_When_Given_Valid_Data()
        {
            //AAA
            var validModel = _tokenModel;
            //_clientLogin.SetupMockEntityRepositoryForGetAll(_clientLoginList);
            var mockRep = new Mock<IRepository<ClientLogin>>();
            mockRep.Setup(x => x.Get()).Returns(_clientLoginList.AsQueryable());
            var mockPersistance = new Mock<IPersistenceService>();
            mockPersistance.Setup(x => x.GetRepository<ClientLogin>()).Returns(mockRep.Object);

            _authTokenConfig.SetupMockEntityRepositoryForGetAll(_authTokenList);
            var service = new SecurityService(_authTokenConfig.MockPersistence.Object, _authTokenConfig.MockLog.Object, _authTokenConfig.MockUtility.Object);
            var result = service.GenerateToken(validModel);
            //_authTokenConfig.MockEntity.VerifyAll();
            _authTokenConfig.MockEntity.Verify(x => x.SaveChanges(true), Times.AtLeastOnce());

        }

        //[TestMethod]

        //public void GenerateToken_Should_Pass_W_Given_Valid_Data()
        //{

        //    string S = "ABBCC";
        //    if (S.Length <= 50000)
        //    {

        //        var prefix = string.Empty;
        //        string ss = string.Empty;
        //        while (S.Length > 2)
        //        {
        //            prefix = S.Substring(0, 2);
        //            S = GetRules(S, prefix);
        //            if (S.Length == 3)
        //                break;

        //        }

        //        if (S.Length > 2)
        //            prefix = prefix.Replace(prefix, GetRules(S, S.Substring(1, 2)));

        //    }
        //}

        //public string GetRules(string S, string prefix)
        //{
        //    switch (prefix)
        //    {
        //        case "AB":
        //            S = S.Replace(prefix, "AA");
        //            break;
        //        case "BA":
        //            S = S.Replace(prefix, "AA");
        //            break;
        //        case "CB":
        //            S = S.Replace(prefix, "CC");
        //            break;
        //        case "BC":
        //            S = S.Replace(prefix, "CC");
        //            break;
        //        case "AA":
        //            S = S.Replace(prefix, "A");
        //            break;
        //        case "CC":
        //            S = S.Replace(prefix, "C");
        //            break;
        //        default:
        //            break;
        //    }
        //    return S;
        //}
    }
}