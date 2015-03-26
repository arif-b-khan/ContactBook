using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Repositories;

namespace ContactBook.Domain.Contexts
{
    public interface IUnitOfWork
    {
        IContactBookRepositoryUow UnitOfWork { get; set; }
    }
}
