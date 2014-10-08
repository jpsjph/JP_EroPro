using Common.Services.ViewModel;
using Domain.Model;
using System;
using System.Collections.Generic;

namespace Ero57_Project.Tests.Helpers
{
    public class TokenHelper
    {
        public static TokenModel GetModel()
        {
            return new TokenModel { ExpirationTime = DateTime.Now, IsActive = true, DateUpdated = DateTime.Now, LoginId = 2, UserName = "TEST", Password = "TestPassword", Token = "TestToken" };

        }
        public static AuthToken GetAuthTokenModel()
        {
            return new AuthToken { AuthTokenID = 1, CreatorID = 111, DateCreated = DateTime.Now, DateUpdated = DateTime.Now, IsActive = true, LoginId = 111, Token = "testtOKEN1", Expiration = DateTime.Now.AddYears(1) };
        }
        public static List<AuthToken> GetTokenModelList()
        {
            return new List<AuthToken> { 
            new AuthToken {AuthTokenID=1,CreatorID=111,DateCreated=DateTime.Now,DateUpdated=DateTime.Now,IsActive=true,LoginId=111,Token="testtOKEN1", Expiration=DateTime.Now.AddYears(1)},
            new AuthToken {AuthTokenID=3,CreatorID=111,DateCreated=DateTime.Now,DateUpdated=DateTime.Now,IsActive=true,LoginId=111,Token="testtOKEN2", Expiration=DateTime.Now.AddYears(2)},
            new AuthToken {AuthTokenID=4,CreatorID=111,DateCreated=DateTime.Now,DateUpdated=DateTime.Now,IsActive=true,LoginId=111,Token="testtOKEN3", Expiration=DateTime.Now.AddYears(3)}
            };
        }

        public static List<ClientLogin> GetClientLoginList()
        {
            return new List<ClientLogin>
            {
                new ClientLogin {ID = 2, UserName = "TEST", Password = "TestPassword", Active = true, Answer1 = "Test"},
                new ClientLogin {ID = 1, UserName = "jps", Password = "tttt", Active = true, Answer1 = "Test2"}
            };
        }
    }
}
