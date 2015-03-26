using ContactBook.Db.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Repositories
{
    public class ContactBookDbRepository<T> : IContactBookRepository<T> where T : class
    {
        DbContext context;
        DbSet<T> dbSet;
        
        public ContactBookDbRepository(ContactBookEdmContainer con)
        {
            this.context = con;
            this.dbSet = context.Set<T>();
        }

        public virtual void Insert(T t)
        {
            dbSet.Add(t);
        }

        public virtual void Delete(T t)
        {
            if (context.Entry(t).State == EntityState.Detached)
            {
                dbSet.Attach(t);
            }
            dbSet.Remove(t);
        }

        public virtual void Update(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }

        public virtual IEnumerable<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string includeProperties = "")
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
                return orderby(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual T GetById(object id)
        {
            return dbSet.Find(id);
        }
    }
}
