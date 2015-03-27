using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;

namespace ContactBook.Domain.Contexts
{
    public interface IUserInfoContext
    {
        AspNetUser GetUserInfo(string userName);
    }
}
