using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ContactBook.Domain.Contexts.Generics
{
    public interface IGenericContextTypes<T, M> where T : class //T = AddressType and M = CB_AddressType
    {
        List<T> GetTypes(Expression<Func<M, bool>> filter);

        List<M> GetCBTypes(Expression<Func<M, bool>> filter);

        void InsertTypes(List<T> typeList);

        void UpdateTypes(M cbExistingType, T cbType);
        
        void DeleteTypes(M cbType);

        T Find(object id);
    }
}