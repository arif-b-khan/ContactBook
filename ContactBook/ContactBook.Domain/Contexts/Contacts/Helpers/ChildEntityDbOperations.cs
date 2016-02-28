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
        IContactBookDbRepository<CB_Contact> contactRepo;
        IContactBookRepositoryUow uow;

        public ChildEntityDbOperations(IContactBookDbRepository<CB_Contact> contactRepository, IContactBookRepositoryUow unitOfWork) 
        {
            contactRepo = contactRepository;
            uow = unitOfWork;
        }

        public void PerformOperations(CB_Contact mContact, CB_Contact dContact)
        {
            DbOperationsCheck<CB_Number>(mContact.CB_Numbers, dContact.CB_Numbers, uow.GetEntityByType<CB_Number>());
            DbOperationsCheck<CB_Email>(mContact.CB_Emails, dContact.CB_Emails, uow.GetEntityByType<CB_Email>());
            DbOperationsCheck<CB_IM>(mContact.CB_IMs, dContact.CB_IMs, uow.GetEntityByType<CB_IM>());
            DbOperationsCheck<CB_Address>(mContact.CB_Addresses, dContact.CB_Addresses, uow.GetEntityByType<CB_Address>());
            DbOperationsCheck<CB_InternetCall>(mContact.CB_InternetCalls, dContact.CB_InternetCalls, uow.GetEntityByType<CB_InternetCall>());
            DbOperationsCheck<CB_Website>(mContact.CB_Websites, dContact.CB_Websites, uow.GetEntityByType<CB_Website>());
            DbOperationsCheck<CB_Relationship>(mContact.CB_Relationships, dContact.CB_Relationships, uow.GetEntityByType<CB_Relationship>());
            DbOperationsCheck<CB_SpecialDate>(mContact.CB_SpecialDates, dContact.CB_SpecialDates,  uow.GetEntityByType<CB_SpecialDate>());
            DbOperationsCheck<CB_ContactByGroup>(mContact.CB_ContactByGroups, dContact.CB_ContactByGroups, uow.GetEntityByType<CB_ContactByGroup>());
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
