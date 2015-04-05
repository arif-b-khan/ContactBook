using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace ContactBook.Db.Repositories
{
    public class ContactBookRepositoryUow : IContactBookRepositoryUow
    {
        private bool disposed = false;
        private DbContext container;
        private IDictionary<string, Type> entityDictionary = new Dictionary<string, Type>();
        private IDictionary<string, object> cachedInstance = new Dictionary<string, object>();

        public ContactBookRepositoryUow(DbContext container)
        {
            this.container = container;

            var types = Assembly.Load("ContactBook.Db").GetTypes()
                .Where(w =>
                {
                    return w.Namespace.Equals("ContactBook.Db.Data");
                });
            foreach (Type itemType in types)
            {
                entityDictionary.Add(itemType.Name, itemType);
            }
        }

        public IContactBookDbRepository<T> GetEntityByType<T>() where T : class
        {
            string key = typeof(T).Name;

            if (entityDictionary.ContainsKey(key))
            {
                if (!cachedInstance.ContainsKey(key))
                {
                    lock (cachedInstance)
                    {
                        if (!cachedInstance.ContainsKey(key))
                        {
                            var contactDbRepoType = typeof(ContactBookDbRepository<>).MakeGenericType(entityDictionary[key]);
                            cachedInstance.Add(key, Activator.CreateInstance(contactDbRepoType, container));
                        }
                    }
                }
                return (ContactBookDbRepository<T>)cachedInstance[key];
            }
            return default(ContactBookDbRepository<T>);
        }

        public bool Save()
        {
            bool result = false;

            try
            {
                container.SaveChanges();
                result = true;
            }
            catch (InvalidOperationException inex)
            {
                throw;
            }
            catch (DbEntityValidationException vdExec)
            {
                foreach (var item in vdExec.EntityValidationErrors)
                {
                    Debug.WriteLine(item.Entry.Entity.ToString());
                    Debug.WriteLine(item.IsValid.ToString());
                    foreach (var error in item.ValidationErrors)
                    {
                        Debug.WriteLine(error.PropertyName);
                        Debug.WriteLine(error.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool managedDisposed)
        {
            if (!disposed)
            {
                if (managedDisposed)
                {
                    container.Dispose();
                }
                disposed = true;
            }
        }
    }
}