using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Repositories
{
    public interface IContactBookDbRepository<T> where T : class
    {
        void Insert(T t);
        void Delete(T t);
        void Update(T t);
        T GetById(object id);
        IEnumerable<T> Get();
        IEnumerable<T> Get(Expression<Func<T, bool>> expression);
        IEnumerable<T> Get(Expression<Func<T, bool>> expression = null, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderby = null);
        IEnumerable<T> Get(Expression<Func<T, bool>> expression = null, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderby = null, string includeProperties = "");

    }
}
