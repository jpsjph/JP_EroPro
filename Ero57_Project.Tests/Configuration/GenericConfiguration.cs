using Common.Services;
using Core.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ero57_API.Tests.Configuration
{
    public class GenericConfiguration<T> where T : class
    {
        public Mock<IPersistenceService> MockPersistence { get; set; }

        public Mock<ILogService> MockLog { get; set; }

        public Mock<IVBUtilities> MockUtility { get; set; }

        public Mock<ISecurityService> MockSecurity { get; set; }

        public Mock<IRepository<T>> MockEntity { get; set; }

        public MockRepository MockRepository { get; set; }

        public void Setup()
        {
            MockRepository = new MockRepository(MockBehavior.Loose) { DefaultValue = DefaultValue.Mock };
            MockPersistence = MockRepository.Create<IPersistenceService>();
            MockUtility = MockRepository.Create<IVBUtilities>();
            MockLog = MockRepository.Create<ILogService>();
            MockSecurity = new Mock<ISecurityService>();
            MockEntity = MockRepository.Create<IRepository<T>>();
        }

        /// <summary>
        /// Setup mock for Get method
        /// </summary>
        /// <param name="results"> </param>
        public void SetupMockEntityRepositoryForGetAll(List<T> results)
        {
            MockEntity.Setup(r => r.Get()).Returns(results.AsQueryable());
            MockPersistence.Setup(p => p.GetRepository<T>()).Returns(MockEntity.Object);
        }

        /// <summary>
        /// Setup mock for Find method
        /// </summary>
        /// <param name="results"> </param>
        public void SetupMockEntityRepositoryForFind(List<T> results)
        {
            MockPersistence.Setup(p => p.GetRepository<T>()).Returns(MockEntity.Object);
            MockEntity.Setup(r => r.Find(It.IsAny<object>())).Returns(results.FirstOrDefault());
        }

        /// <summary>
        /// Setup mock for FindAsync method
        /// </summary>
        /// <param name="results"> </param>
        public void SetupMockEntityRepositoryForFindAsync(List<T> results)
        {
            MockPersistence.Setup(p => p.GetRepository<T>()).Returns(MockEntity.Object);
            MockEntity.Setup(r => r.FindAsync(It.IsAny<object>())).Returns(Task.FromResult<T>(null));
        }

        public void SetupMocForPersistence()
        {
            MockPersistence.Setup(p => p.GetRepository<T>()).Returns(MockEntity.Object);
        }
    }
}