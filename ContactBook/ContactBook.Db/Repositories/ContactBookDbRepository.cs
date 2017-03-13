using ContactBook.Db.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ContactBook.Db.Repositories
{
    public class ContactBookDbRepository<T> : IContactBookDbRepository<T> where T : class
    {
        private DbContext context;
        private DbSet<T> dbSet;
        private object lockObj = new object();

        public ContactBookDbRepository(ContactbookDbContext con)
        {
            this.context = con;
            this.dbSet = context.Set<T>();
        }

        public virtual void Insert(T t)
        {
            lock (lockObj)
            {
                dbSet.Add(t);
            }
        }

        public virtual void Delete(T t)
        {
            lock (lockObj)
            {
                if (context.Entry(t).State == EntityState.Detached)
                {
                    dbSet.Attach(t);
                }
                dbSet.Remove(t);
            }
        }

        public virtual void Update(T t)
        {
            lock (lockObj)
            {
                dbSet.Attach(t);
                context.Entry(t).State = EntityState.Modified;
            }
        }

        public virtual void Update(T existingObj, T newObj)
        {
            lock (lockObj)
            {
                context.Entry(existingObj).CurrentValues.SetValues(newObj);
            }
        }

        public T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderby = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
               (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderby != null)
            {
                return orderby.Compile().Invoke(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual IEnumerable<T> Get()
        {
            return Get(null);
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> expression)
        {
            return Get(expression, null);
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> expression = null, Expression<Func<IQueryable<T>, IOrderedQueryable<T>>> orderby = null)
        {
            return Get(expression, orderby, "");
        }
    }
}