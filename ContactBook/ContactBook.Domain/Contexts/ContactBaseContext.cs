using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;

namespace ContactBook.Domain.Contexts
{
    public abstract class ContactBaseContext : IDisposable
    {
        string connectionName;
        bool disposed = false;

        ContactBookEdmContainer container;
        public ContactBaseContext()
            : this("")
        {

        }

        public ContactBaseContext(string connection)
        {
            this.connectionName = connection;
        }

        protected ContactBookEdmContainer GetContainer
        {
            get
            {
                ContactBookEdmContainer container;
                if (string.IsNullOrEmpty(connectionName))
                {
                    container = new ContactBookEdmContainer();
                }
                else
                {
                    container = new ContactBookEdmContainer(connectionName: "name=" + connectionName);
                }
                return container;
            }
        }

        public void Dispose()
        {
            if (!disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }

        protected virtual void Dispose(bool managedDispose)
        {
            if (managedDispose)
            {
                container.Dispose();
            }
        }
    }
}
