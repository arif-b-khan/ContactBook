using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.IoC;

namespace ContactBook.Domain.Contexts
{
    public class ContactBookSecretContext : ContactBook.Domain.Contexts.IContactBookSecretContext
    {
        private IContactBookRepositoryUow unitOfWork;
        private IContactBookDbRepository<CB_Secret> conSecret;

        public ContactBookSecretContext() : this(DependencyFactory.Resolve<IContactBookRepositoryUow>()) { }

        public ContactBookSecretContext(IContactBookRepositoryUow pUnitofwork)
        {
            unitOfWork = pUnitofwork;
            conSecret = unitOfWork.GetEntityByType<CB_Secret>();
        }

        public Dictionary<string, string> GetKeyValue()
        {
            Dictionary<string, string> retDic = new Dictionary<string, string>();

            var secret = conSecret.Get().Single();
            
            if (secret != null)
            {
                retDic.Add("SecretKey", secret.SecretKey);
                retDic.Add("SecretValue", secret.SecretValue);
            }

            return retDic;
        }
    }
}
