using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;

namespace ContactBook.Domain.Contexts
{
    public class UserInfoContext : ContactBaseContext, IUserInfoContext
    {
        
        public UserInfoContext()
            : this("")
        {
        }

        public UserInfoContext(string connection)
            : base(connection)
        {
            UnitOfWork = new ContactBookRepositoryUow(GetContainer);
        }

        public IContactBookRepositoryUow UnitOfWork
        {
            get;
            set;
        }

        public AspNetUser GetUserInfo(string userName)
        {
            ContactBookDbRepository<AspNetUser> user = UnitOfWork.GetEntityByType<AspNetUser>();
            return user.Get(u => u.UserName == userName).FirstOrDefault();
        }

        protected override void Dispose(bool managedDispose)
        {
            base.Dispose(managedDispose);
            UnitOfWork.Dispose();
        }
    }
}
