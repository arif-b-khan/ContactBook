using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;

namespace ContactBook.Domain.Contexts
{
    public class UserInfoContext : IUserInfoContext
    {
        IContactBookRepositoryUow unitOfWork;
        IContactBookDbRepository<AspNetUser> user;
        public UserInfoContext(IContactBookRepositoryUow unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            user = unitOfWork.GetEntityByType<AspNetUser>();
        }
        public AspNetUser GetUserInfo(string userName)
        {
            AspNetUser retuser = user.Get(usr => usr.UserName == userName).FirstOrDefault();
            return retuser;
        }
    }
}
