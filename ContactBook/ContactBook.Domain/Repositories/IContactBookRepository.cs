using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Repositories
{
    public interface IContactBookRepository<T> where T : class
    {
        void Insert(T t);
        void Delete(T t);
        void Update(T t);
        IEnumerable<T> Get(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = "");
        T GetById(object id);
    }
}
