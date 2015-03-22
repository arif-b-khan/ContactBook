using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db
{
    public partial class ContactBookEdmContainer
    {
        public ContactBookEdmContainer(string connectionName = "name=ContactBookEdmContainer")
            : base(connectionName)
        {

        }
    }
}
