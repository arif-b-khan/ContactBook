using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using NLog;

namespace ContactBook.Db.Repositories
{
    public class ContactBookRepositoryUow : IContactBookRepositoryUow
    {
        private bool disposed = false;
        private DbContext container;
        private IDictionary<string, Type> entityDictionary = new Dictionary<string, Type>();
        private static readonly Logger _logger;

        static ContactBookRepositoryUow()
        {
            _logger = LogManager.GetLogger("ContactBook.Db");
        }

        public ContactBookRepositoryUow(DbContext container)
        {
            this.container = container;

            container.Database.Log = s =>
            {
                System.Diagnostics.Debug.Write(s);
            };

            var types = Assembly.Load("ContactBook.Db").GetTypes()
                .Where(w =>
                {
                    return w.Namespace.Equals("ContactBook.Db.Data");
                });

            _logger.Info("ContactBookRepositryUow: Registering Context");
            foreach (Type itemType in types)
            {
                entityDictionary.Add(itemType.Name, itemType);
                _logger.Info(string.Format("Name: {0}, Type: {1}", itemType.Name, itemType.ToString()));
            }
        }

        public IContactBookDbRepository<T> GetEntityByType<T>() where T : class
        {
            string key = typeof(T).Name;
            var contactDbRepoType = typeof(ContactBookDbRepository<>).MakeGenericType(entityDictionary[key]);
            return (ContactBookDbRepository<T>)Activator.CreateInstance(contactDbRepoType, container);
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
                _logger.Error(inex.Message, inex);
                throw;
            }
            catch (DbEntityValidationException vdExec)
            {
                StringBuilder errorbuilder = new StringBuilder();
                foreach (var item in vdExec.EntityValidationErrors)
                {
                    errorbuilder.Append(string.Format("Entity Name: {0}, IsValue: {1}", item.Entry.Entity.ToString(), item.IsValid.ToString()));
                    errorbuilder.AppendLine();
                    errorbuilder.Append("ValidationErrors: ");
                    foreach (var error in item.ValidationErrors)
                    {
                        errorbuilder.Append(string.Format("PropertyName:{0}, ErrorMessage: {1}", error.PropertyName, error.ErrorMessage));
                        errorbuilder.AppendLine();
                    }
                }

                _logger.Error(string.Format("MethodName:{0} Message:{1}", vdExec.TargetSite.Name, errorbuilder.ToString()), vdExec);
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
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