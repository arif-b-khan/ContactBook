using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Repositories;
using Moq;

namespace ContactBook.WebApi.Test.Fixtures
{
    public class ControllerTestFixtures
    {
        Mock<IContactBookRepositoryUow> mockUnitOfWork = new Mock<IContactBookRepositoryUow>();

        public ControllerTestFixtures()
        {

        }

        public IContactBookDbRepository<T> MockRepository<T>(List<T> plist) where T : class
        {
            Mock<IContactBookDbRepository<T>> repository = new Mock<IContactBookDbRepository<T>>();
            repository.Setup(rp => rp.Get()).Returns(plist);
            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>())).Returns(plist);
            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>())).Returns(plist);
            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>(), "")).Returns(plist);
            return repository.Object;
        }

        public Mock<IContactBookRepositoryUow> MockUnitOfWork
        {
            get { return mockUnitOfWork; }
            private set { mockUnitOfWork = value; }
        }

    }
}
