using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ContactBook.Domain.Contexts.Generics
{
    public interface IGenericContextTypes<T, M> where T : class
    {
        List<T> GetTypes(Expression<Func<M, bool>> filter);

        void InsertTypes(List<T> typeList);

        void UpdateTypes(List<T> typeList);

        void DeleteTypes(List<T> typeList);

        T Find(object id);
    }
}