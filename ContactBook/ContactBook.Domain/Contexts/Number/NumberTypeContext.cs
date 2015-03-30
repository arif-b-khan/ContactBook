using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Models;

namespace ContactBook.Domain.Contexts.Number
{
    public class NumberTypeContext
    {
        IContactBookRepositoryUow unitOfWork;
        IContactBookDbRepository<CB_NumberType> numberRespository;

        public NumberTypeContext(IContactBookRepositoryUow pUnitOfWork)
        {
            unitOfWork = pUnitOfWork;
            numberRespository = unitOfWork.GetEntityByType<CB_NumberType>();
        }

        public List<MdlNumberType> GetNumberTypes(long bookId)
        {
            List<MdlNumberType> retNumberType = null;

            IEnumerable<CB_NumberType> numberTypes = numberRespository.Get(nmt => ((nmt.BookId.HasValue && nmt.BookId.Value == bookId) || !nmt.BookId.HasValue));

            Mapper.CreateMap<CB_NumberType, MdlNumberType>()
                .ForMember(md => md.NumberType, cb => cb.MapFrom(m => m.Number_TypeName))
                .ForSourceMember(cb => cb.CB_ContactBook, c => c.Ignore())
                .ForSourceMember(cb => cb.CB_Numbers, c => c.Ignore());

            if (numberTypes != null)
            {
                retNumberType = Mapper.Map<List<MdlNumberType>>(numberTypes);
            }

            return retNumberType;
        }

        public void InsertNumberType(List<MdlNumberType> numberType)
        {
            Mapper.CreateMap<MdlNumberType, CB_NumberType>()
                .ForMember(md => md.Number_TypeName, cb => cb.MapFrom(m => m.NumberType));
            List<CB_NumberType> numberTypeList = Mapper.Map<List<CB_NumberType>>(numberType);
            
            foreach(CB_NumberType typeItem in numberTypeList)
            {
                numberRespository.Insert(typeItem);
            }
        }

        public void UpdateNumberType(List<MdlNumberType> numberType)
        {
            Mapper.CreateMap<MdlNumberType, CB_NumberType>()
                .ForMember(md => md.Number_TypeName, cb => cb.MapFrom(m => m.NumberType));
            List<CB_NumberType> numberTypeList = Mapper.Map<List<CB_NumberType>>(numberType);

            foreach (CB_NumberType typeItem in numberTypeList)
            {
                numberRespository.Update(typeItem);
            }
        }

        public void DeleteNumberType(List<MdlNumberType> numberType)
        {
            Mapper.CreateMap<MdlNumberType, CB_NumberType>()
                .ForMember(md => md.Number_TypeName, cb => cb.MapFrom(m => m.NumberType));
            List<CB_NumberType> numberTypeList = Mapper.Map<List<CB_NumberType>>(numberType);

            foreach (CB_NumberType typeItem in numberTypeList)
            {
                numberRespository.Delete(typeItem);
            }
        }
    }
}
