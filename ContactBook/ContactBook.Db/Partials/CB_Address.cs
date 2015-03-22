using ContactBook.Db.Implementation;
using ContactBook.Domain.Entities;
using ContactBook.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db
{
    public partial class CB_Address : ContactBookDbRepository<CB_Address>, IEntity
    {
        public CB_Address()
            : base(new ContactBookEdmContainer())
        {

        }
    }
}
