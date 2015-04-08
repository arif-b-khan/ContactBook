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
            DbOperations<CB_Number>(mContact.CB_Numbers, dContact.CB_Numbers, _unitOfWork.GetEntityByType<CB_Number>(), CB_Number.Comparer);
            DbOperations<CB_Email>(mContact.CB_Emails, dContact.CB_Emails, _unitOfWork.GetEntityByType<CB_Email>(), CB_Email.Comparer);
            DbOperations<CB_IM>(mContact.CB_IMs, dContact.CB_IMs, _unitOfWork.GetEntityByType<CB_IM>(), CB_IM.Comparer);
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
                            dbRepo.Delete(item.Clone(item));
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
