using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
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
            repository.Setup(rp => rp.Get()).Returns(() => {
                return plist;
            });
            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>())).Returns<Expression<Func<T, bool>>>(e => 
                {
                    if (plist != null)
                    {
                        return plist.Where(e.Compile());
                    }
                    else
                    {
                        return plist;
                    }
                }
                );
            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>())).Returns<Expression<Func<T, bool>>, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>((wh, od) => {
                if (plist == null)
                {
                    return null;
                }
                return od.Compile().Invoke(plist.Where(wh.Compile()).AsQueryable()).AsEnumerable();
            });
            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>(), It.IsAny<string>())).Returns<Expression<Func<T, bool>>, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>, string>((wh, od, str) =>
            {
                if (plist == null)
                {
                    return null;
                }
                return od.Compile().Invoke(plist.Where(wh.Compile()).AsQueryable()).AsEnumerable();
            });
            return repository.Object;
        }
        public Mock<IContactBookDbRepository<T>> MockRepository<T>() where T : class
        {
            return new Mock<IContactBookDbRepository<T>>();
        }
        public Mock<IContactBookDbRepository<T>> MockRepositoryNeedList<T>(List<T> plist) where T : class
        {
            Mock<IContactBookDbRepository<T>> repository = new Mock<IContactBookDbRepository<T>>();
            repository.Setup(rp => rp.Get()).Returns(() =>
            {
                return plist;
            });
            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>())).Returns<Expression<Func<T, bool>>>(e =>
            {
                if (plist != null)
                {
                    return plist.Where(e.Compile());
                }
                else
                {
                    return plist;
                }
            }
                );
            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>())).Returns<Expression<Func<T, bool>>, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>((wh, od) =>
            {
                if (plist == null)
                {
                    return null;
                }
                return od.Compile().Invoke(plist.Where(wh.Compile()).AsQueryable()).AsEnumerable();
            });
            repository.Setup(rp => rp.Get(It.IsAny<Expression<Func<T, bool>>>(), It.IsAny<Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>>(), It.IsAny<string>())).Returns<Expression<Func<T, bool>>, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>>, string>((wh, od, str) =>
            {
                if (plist == null)
                {
                    return null;
                }
                return od.Compile().Invoke(plist.Where(wh.Compile()).AsQueryable()).AsEnumerable();
            });
            repository.Setup(rp => rp.GetById(It.IsAny<object>())).Returns<object>(t => {
                if (plist == null)
                {
                    return null;
                }
                return plist.LastOrDefault();
            });
            return repository;
        }


        public Mock<IContactBookRepositoryUow> MockUnitOfWork
        {
            get { return mockUnitOfWork; }
            private set { mockUnitOfWork = value; }
        }

        public HttpResponseMessage GetResponseMessage(IHttpActionResult actResult, CancellationToken ct)
        {
            return actResult.ExecuteAsync(ct).Result;
        }

        public void RouteConfig(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}"); 
        }
    }
}
