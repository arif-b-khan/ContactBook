using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Repositories
{
    public interface IContactBookRepository<T> where T : class
    {
        bool AddItems(List<T> t);
        bool Add(T t);
        bool Delete(T t);
        bool DeleteById(int id);
        bool Update(T t);
        List<T> GetItems<T>();
        T GetItem<T>();
    }
}
