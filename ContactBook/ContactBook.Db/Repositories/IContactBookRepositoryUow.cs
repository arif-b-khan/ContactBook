using System;
namespace ContactBook.Db.Repositories
{
    public interface IContactBookRepositoryUow
    {
        ContactBookDbRepository<T> GetEntityByType<T>() where T : class;
        bool Save();
    }
}
