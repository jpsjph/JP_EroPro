using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.Unity;
using Core.Infrastructure;
using Core.Concrete;
using System.Web.Http;
using System.Net.Http;
using System.Web.Http.Controllers;



namespace Ero57_Project.Tests
{
    [TestClass]
    public class DependencyInjectionTest
    {
        DataContext _context;

        [TestInitialize]
        public void TearUp()
        {
            _context = new DataContext("connectionString");
        }

        [TestMethod]
        public void UnityResolver_Resolves_Registered_DBContect_Test()
        {
            var container = new UnityContainer();
            container.RegisterInstance<IDataContext>(_context);
            var resolver = new UnityResolver(container);
            var instance = resolver.GetService(typeof(IDataContext));
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void UnityResolver_Resolves_Registered_PersistenceService_Test()
        {
            var container = new UnityContainer();
            container.RegisterInstance<IPersistenceService>(new PersistenceService(_context));
            var resolver = new UnityResolver(container);
            var instance = resolver.GetService(typeof(IPersistenceService));
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void UnityResolver_Resolves_Registered_EmailService_Test()
        {
            var container = new UnityContainer();
            container.RegisterInstance<IEmailService>(new EmailService());
            var resolver = new UnityResolver(container);
            var instance = resolver.GetService(typeof(IEmailService));
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void UnityResolver_Resolves_Registered_LogService_Test()
        {
            var container = new UnityContainer();
            container.RegisterInstance<ILogService>(new LogService("ConnectionString", new EmailService()));
            var resolver = new UnityResolver(container);
            var instance = resolver.GetService(typeof(ILogService));
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void UnityResolver_Resolves_Registered_Utilities_Test()
        {
            var container = new UnityContainer();
            container.RegisterInstance<IUtilities>(new Utilities(new PersistenceService(_context),new LogService("ConnectionString", new EmailService())));
            var resolver = new UnityResolver(container);
            var instance = resolver.GetService(typeof(IUtilities));
            Assert.IsNotNull(instance);
        }
        [TestMethod]
        public void UnityResolver_Resolves_Registered_PersistenceService_Through_TransactionController_Test()
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
            var container = new UnityContainer();
            container.RegisterInstance<IPersistenceService>(new PersistenceService(_context));
            container.RegisterInstance<ILogService>(new LogService("ConnectionString", new EmailService()));
            var server = new HttpServer(config);
            var client = new HttpClient(server);
            client.GetAsync("http://localhost/Ero57_Project/api/transaction/1").ContinueWith(task =>
                    {
                        var response = task.Result;
                        Assert.IsNotNull(response.Content);
                    });
        }

        [TestMethod]
        public void UnityResolver_In_HttpConfig_DoesNot_Resolve_PipelineType_But_Fallback_To_DefaultResolver_Test()
        {
            var container = new UnityContainer();

            var config = new HttpConfiguration {DependencyResolver = new UnityResolver(container)};
            var instance = config.Services.GetService(typeof(IHttpActionSelector));
            Assert.IsNotNull(instance);
        }
    }
}
