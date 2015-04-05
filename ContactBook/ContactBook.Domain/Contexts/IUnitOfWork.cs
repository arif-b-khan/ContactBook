using ContactBook.Db.Repositories;

namespace ContactBook.Domain.Contexts
{
    public interface IUnitOfWork
    {
        IContactBookRepositoryUow UnitOfWork { get; set; }
    }
}