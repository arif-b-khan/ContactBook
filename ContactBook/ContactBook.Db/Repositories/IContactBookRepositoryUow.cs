using System;
namespace ContactBook.Db.Repositories
{
    public interface IContactBookRepositoryUow : IDisposable
    {
        IContactBookDbRepository<T> GetEntityByType<T>() where T : class;
        bool Save();
    }
}
