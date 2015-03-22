using ContactBook.Db.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class CB_Address : ContactBookDbRepository<CB_Address>
    {
        public CB_Address()
            : base(new ContactBookEdmContainer())
        {

        }
    }
}
