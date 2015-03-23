using ContactBook.Db.Data;
using ContactBook.Db.Implementation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Reflection;

namespace ContactBook.Db.Repositories
{
    public class ContactBookRepositoryUow : IDisposable
    {
        bool disposed = false;
        DbContext container;
        IDictionary<string, Type> entityDictionary = new Dictionary<string, Type>();
        IDictionary<string, object> cachedInstance = new Dictionary<string, object>();

        public ContactBookRepositoryUow(DbContext container)
        {
            this.container = container;

            var types = Assembly.Load("ContactBook.Db").GetTypes()
                .Where(w =>
                {
                    var type = typeof(ContactBookDbRepository<>).MakeGenericType(w);
                    return type.IsAssignableFrom(w);
                });

            foreach (Type itemType in types)
            {
                entityDictionary.Add(itemType.Name, itemType);
            }
        }

        public T GetEntityByType<T>()
        {
            string key = typeof(T).Name;

            if (entityDictionary.ContainsKey(key))
            {
                if (!cachedInstance.ContainsKey(key))
                {
                    cachedInstance.Add(key, Activator.CreateInstance(entityDictionary[key], container));
                }
                return (T)cachedInstance[key];
            }

            return default(T);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool managedDisposed)
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
