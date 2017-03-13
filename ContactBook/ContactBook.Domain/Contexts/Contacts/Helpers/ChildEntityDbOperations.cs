using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;

namespace ContactBook.Domain.Contexts.Contacts.Helpers
{

    public class ChildEntityDbOperations
    {
        IContactBookDbRepository<Contact> contactRepo;
        IContactBookRepositoryUow uow;

        public ChildEntityDbOperations(IContactBookDbRepository<Contact> contactRepository, IContactBookRepositoryUow unitOfWork) 
        {
            contactRepo = contactRepository;
            uow = unitOfWork;
        }

        public void PerformOperations(Contact mContact, Contact dContact)
        {
            DbOperationsCheck<Number>(mContact.Numbers, dContact.Numbers, uow.GetEntityByType<Number>());
            DbOperationsCheck<Email>(mContact.Emails, dContact.Emails, uow.GetEntityByType<Email>());
            DbOperationsCheck<IM>(mContact.IMs, dContact.IMs, uow.GetEntityByType<IM>());
            DbOperationsCheck<Db.Data.Address>(mContact.Addresses, dContact.Addresses, uow.GetEntityByType<Db.Data.Address>());
            DbOperationsCheck<InternetCall>(mContact.InternetCalls, dContact.InternetCalls, uow.GetEntityByType<InternetCall>());
            DbOperationsCheck<Website>(mContact.Websites, dContact.Websites, uow.GetEntityByType<Website>());
            DbOperationsCheck<Relationship>(mContact.Relationships, dContact.Relationships, uow.GetEntityByType<Relationship>());
            DbOperationsCheck<SpecialDate>(mContact.SpecialDates, dContact.SpecialDates,  uow.GetEntityByType<SpecialDate>());
            DbOperationsCheck<ContactByGroup>(mContact.ContactByGroups, dContact.ContactByGroups, uow.GetEntityByType<ContactByGroup>());
        }

        private void DbOperationsCheck<T>(ICollection<T> collection1, ICollection<T> collection2, IContactBookDbRepository<T> actualEntity) where T : class, INewEntity<T>, IEntityCloneable<T>, IEquatable<T>
        {
            if (collection1 != null && collection2 != null && (collection1.Any() || collection2.Any()))
            {
                DbOperations<T>(collection1, collection2, actualEntity, ComparerFactory.GetComparer<T>());
            }
        }

        private void DbOperations<T>(ICollection<T> collection1, ICollection<T> collection2, IContactBookDbRepository<T> actualEntity, IEqualityComparer<T> tcomparer) where T : class, INewEntity<T>, IEntityCloneable<T>, IEquatable<T>
        {
            try
            {
                // populate insert collections
                foreach (T item in collection1.Where(t => t.Equals(0)))
                {
                    actualEntity.Insert(item.Clone());
                }

                IList<T> removeList = new List<T>();
                foreach (T item in collection2.Except(collection1, tcomparer))
                {
                    removeList.Add(item);
                }

                foreach(T item in removeList)
                {
                    actualEntity.Delete(item);
                }

                foreach (T item in collection1.Intersect(collection2, tcomparer))
                {
                    T oldEntity = collection2.Where(t => tcomparer.Equals(t, item)).SingleOrDefault();
                    if (oldEntity != null && !oldEntity.Equals(item))
                    {
                        actualEntity.Update(oldEntity, item);
                    }
                }
            }
            catch (Exception aggException)
            {

            }
        }
    }
}
