using ContactBook.Db.Implementation;
using ContactBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db
{
    public partial class AspNetUser : ContactBookDbRepository<AspNetUser>, IEntity
    {
        
    }
}
