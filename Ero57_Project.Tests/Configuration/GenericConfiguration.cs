using Core.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ero57_Project.Tests.Configuration
{
    public class GenericConfiguration<T> where T : EntityBase
    {

        public Mock<IPersistenceService> MockPersistence { get; set; }
        public Mock<ILogService> MockLog { get; set; }
        public Mock<IReadOnlyRepository<T>> MockReadOnlyRepository { get; set; }
        public Mock<IRepository<T>> MockEntity { get; set; }
        public MockRepository MockRepository { get; set; }

        public void Setup()
        {
            MockRepository = new MockRepository(MockBehavior.Loose) { DefaultValue = DefaultValue.Mock };
            MockPersistence = MockRepository.Create<IPersistenceService>();
            MockLog = MockRepository.Create<ILogService>();
            MockEntity = MockRepository.Create<IRepository<T>>();
            MockReadOnlyRepository = MockRepository.Create<IReadOnlyRepository<T>>();
        }

        /// <summary>
        /// Setup mock for findby method
        /// </summary>
        /// <param name="result"></param>

        public void SetupMockEntityRepositoryForSqlQuery(List<T> result)
        {
            MockEntity.Setup(r => r.SqlQuery(It.IsAny<string>(), It.IsAny<object>())).Returns(result.AsQueryable());
            SetupMocForPersistence();
        }

        /// <summary>
        /// Setup mock for findby method
        /// </summary>
        /// <param name="result"></param>

        public void SetupMockEntityRepositoryForFindBy(List<T> result)
        {
            MockEntity.Setup(r => r.FindBy(It.IsAny<Expression<Func<T, bool>>>())).Returns(result.AsQueryable());
            SetupMocForPersistence();
        }

        /// <summary>
        /// Setup mock for Find method
        /// </summary>
        /// <param name="results"> </param>
        public void SetupMockEntityRepositoryForFind(List<T> results)
        {
            SetupMocForPersistence();
            MockEntity.Setup(r => r.Find(It.IsAny<object>())).Returns(new Mock<T>().Object);
        }

        /// <summary>
        /// Setup mock for FindAsync method
        /// </summary>
        /// <param name="results"> </param>
        public void SetupMockEntityRepositoryForFindAsync(List<T> results)
        {
            SetupMocForPersistence();
            MockEntity.Setup(r => r.FindAsync(It.IsAny<object>())).Returns(Task.FromResult<T>(results.FirstOrDefault()));
        }
        
        /// Setup  mock for readonly findby method
        /// </summary>
        /// <param name="results"> </param>
        public void SetupReadOnlyRepositoryForFindBy(List<T> results)
        {
            SetupMocForPersistence();
            MockReadOnlyRepository.Setup(r => r.FindBy(It.IsAny<Expression<Func<T, bool>>>())).Returns(results.AsQueryable());
        }

        /// <summary>
        /// Setup mock readyonly for find method
        /// </summary>
        /// <param name="result"></param>
        public void SetupReadOnlyRepositoryForFind(List<T> result)
        {
            SetupMocForPersistence();
            MockReadOnlyRepository.Setup(r => r.GetAll()).Returns(result.AsQueryable());
        }

        /// <summary>
        /// Setup mock for persistence
        /// </summary>
        public void SetupMocForPersistence()
        {
            MockPersistence.Setup(p => p.GetRepository<T>()).Returns(MockEntity.Object);
        }
    }
}