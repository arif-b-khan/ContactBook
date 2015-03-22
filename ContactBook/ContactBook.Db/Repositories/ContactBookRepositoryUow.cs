using ContactBook.Db.Data;
using ContactBook.Db.Implementation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Repositories
{
    public class ContactBookRepositoryUow : IDisposable
    {
        bool disposed = false;
        DbContext container;
        Lazy<ContactBookDbRepository<CB_Address>> lazyAddress;
        
        public ContactBookRepositoryUow(DbContext container)
        {
            this.container = container;
            lazyAddress = new Lazy<ContactBookDbRepository<CB_Address>>(() =>
            {
                return new ContactBookDbRepository<CB_Address>(container as ContactBookEdmContainer);
            });
        }

        public ContactBookDbRepository<CB_Address> Address
        {
            get
            {
                return lazyAddress.Value;
            }
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
