using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Repositories;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using ContactBook.Domain.Models.ModelMapper;

namespace ContactBook.Domain.Contexts.Generics
{
    public class GenericContextTypes<M, T> : IGenericContextTypes<M, T>
        where M : class
        where T : class
    {
        IContactBookRepositoryUow unitOfWork;
        IContactBookDbRepository<T> repoType;
        ModelMapper mapper = null;

        public GenericContextTypes()
            : this(DependencyFactory.Resolve<IContactBookRepositoryUow>())
        {

        }

        public GenericContextTypes(IContactBookRepositoryUow unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            repoType = this.unitOfWork.GetEntityByType<T>();
            mapper = new ModelMapper();
        }

        public void InsertTypes(List<M> typeList)
        {
            List<T> addrTypeList = mapper.GetMappedType<M, T>(typeList);

            foreach (var item in addrTypeList)
            {
                repoType.Insert(item);
            }

            unitOfWork.Save();
        }

        public void UpdateTypes(List<M> typeList)
        {
            List<T> addrTypeList = mapper.GetMappedType<M, T>(typeList);

            foreach (var item in addrTypeList)
            {
                repoType.Update(item);
            }
            unitOfWork.Save();
        }

        public void DeleteTypes(List<M> typeList)
        {
            List<T> addrTypeList = mapper.GetMappedType<M, T>(typeList);

            foreach (var item in addrTypeList)
            {
                repoType.Delete(item);
            }
            unitOfWork.Save();
        }

        public List<M> GetTypes(Expression<Func<T, bool>> filter)
        {
            List<M> retTypes = null;
            var repoTypes = repoType.Get(filter);

            if (repoTypes != null)
            {
                retTypes = mapper.GetMappedType<T, M>(repoTypes.ToList());
            }

            return retTypes;
        }

        public M Find(object id)
        {
            M retType = null;
            T repType = repoType.GetById(id);
            if (repoType != null)
            {
                retType = mapper.GetMappedType<T, M>(repType);
            }
            return retType;
        }
    }
}
