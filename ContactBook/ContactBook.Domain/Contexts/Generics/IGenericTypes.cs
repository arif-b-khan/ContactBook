using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Contexts.Generics
{
    public interface IGenericTypes<T, M>  where T : class
    {
        List<T> GetTypes(Expression<Func<M, bool>> filter);
        void InsertTypes(List<T> typeList);
        void UpdateTypes(List<T> typeList);
        void DeleteTypes(List<T> typeList);
    }
}
