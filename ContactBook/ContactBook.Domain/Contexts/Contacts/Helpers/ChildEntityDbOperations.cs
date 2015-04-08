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
        IContactBookRepositoryUow _unitOfWork;

        public ChildEntityDbOperations(IContactBookRepositoryUow unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void PerformOperations(CB_Contact mContact, CB_Contact dContact)
        {
            DbOperationsCheck<CB_Number>(mContact.CB_Numbers, dContact.CB_Numbers);
            DbOperationsCheck<CB_Email>(mContact.CB_Emails, dContact.CB_Emails);
            DbOperationsCheck<CB_IM>(mContact.CB_IMs, dContact.CB_IMs);
            DbOperationsCheck<CB_Address>(mContact.CB_Addresses, dContact.CB_Addresses);
            DbOperationsCheck<CB_InternetCall>(mContact.CB_InternetCalls, dContact.CB_InternetCalls);
            DbOperationsCheck<CB_Website>(mContact.CB_Websites, dContact.CB_Websites);
            DbOperationsCheck<CB_Relationship>(mContact.CB_Relationships, dContact.CB_Relationships);
            DbOperationsCheck<CB_SpecialDate>(mContact.CB_SpecialDates, dContact.CB_SpecialDates);
            DbOperationsCheck<CB_ContactByGroup>(mContact.CB_ContactByGroups, dContact.CB_ContactByGroups);
        }

        private void DbOperationsCheck<T>(ICollection<T> collection1, ICollection<T> collection2) where T : class, INewEntity<T>, IEntityCloneable<T>, IEquatable<T>
        {
            if (collection1 != null && collection2 != null && (collection1.Any() || collection2.Any()))
            {
                DbOperations<T>(collection1, collection2, _unitOfWork.GetEntityByType<T>(), ComparerFactory.GetComparer<T>());
            }
        }

        private void DbOperations<T>(ICollection<T> collection1, ICollection<T> collection2, IContactBookDbRepository<T> dbRepo, IEqualityComparer<T> tcomparer) where T : class, INewEntity<T>, IEntityCloneable<T>, IEquatable<T>
        {
            try
            {
                Task insertTask = Task.Run(() =>
                {
                    if (collection1 != null)
                    {
                        // populate insert collections
                        foreach (T item in collection1.Where(t => t.Equals(0)))
                        {
                            dbRepo.Insert(item);
                        }
                    }
                });

                Task deleteTask = Task.Run(() =>
                {
                    if (collection1 != null && collection2 != null)
                    {
                        //populate delete collections
                        foreach (T item in collection2.Except(collection1, tcomparer))
                        {
                            dbRepo.Delete(item.Clone());
                        }
                    }
                });

                Task updateTask = Task.Run(() =>
                {
                    if (collection1 != null && collection2 != null)
                    {
                        foreach (T item in collection1.Intersect(collection2, tcomparer))
                        {
                            T singleEmail = collection2.Where(t => tcomparer.Equals(t, item)).SingleOrDefault();
                            if (singleEmail != null && !singleEmail.Equals(item))
                            {
                                dbRepo.Update(item);
                            }
                        }
                    }
                });

                Task.WaitAll(insertTask, updateTask, deleteTask);
            }
            catch (AggregateException aggException)
            {
                foreach (Exception ex in aggException.InnerExceptions)
                {

                }
            }
        }

    }

}
