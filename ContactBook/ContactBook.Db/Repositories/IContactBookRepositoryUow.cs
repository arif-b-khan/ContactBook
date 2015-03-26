using System;
namespace ContactBook.Db.Repositories
{
    public interface IContactBookRepositoryUow : IDisposable
    {
        ContactBookDbRepository<T> GetEntityByType<T>() where T : class;
        bool Save();
    }
}
