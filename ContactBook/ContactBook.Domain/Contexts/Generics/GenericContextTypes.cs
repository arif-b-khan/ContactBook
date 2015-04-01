using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Models;
using ContactBook.Domain.Models.ModelMapper;

namespace ContactBook.Domain.Contexts.Generics
{
    public class GenericContextTypes<M, T> : IGenericContextTypes<M, T> where M: class where T : class
    {
        IContactBookRepositoryUow unitOfWork;
        IContactBookDbRepository<T> repoType;
        ModelMapper mapper = null;

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
            List<T> repoTypes = repoType.Get(filter).ToList();
            retTypes = mapper.GetMappedType<T, M>(repoTypes);
            return retTypes;
        }
    }
}
