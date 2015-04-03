using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;
using AutoMapper;

namespace ContactBook.Domain.Models.ModelMapper
{

    public class ModelMapper
    {
        public List<D> GetMappedType<T, D>(List<T> t)
        {
            string convertType = typeof(T).Name + "-to-" + typeof(D).Name;

            switch (convertType)
            {
                case "CB_AddressType-to-AddressType":
                    return AddressTypeMapper(t as List<CB_AddressType>) as List<D>;
                    break;

                case "AddressType-to-CB_AddressType":
                    return MdlAddressTypeToAddressType(t as List<AddressType>) as List<D>;
                    break;
                case "NumberType-to-CB_NumberType":
                    return MdlNumberTypeToCBNumberType(t as List<NumberType>) as List<D>;
                    break;
                case "CB_NumberType-to-NumberType":
                    return CBNumberTypeToMdlModelType(t as List<CB_NumberType>) as List<D>;
                    break;
                default:
                    throw new ModelMapperException(string.Format("Mapping method not available.\nPlease add mapping method to convert the type from {0} to {1}", typeof(T).Name, typeof(D).Name));
                    break;
            }

            return default(List<D>);
        }

        public D GetMappedType<T, D>(T t)
        {
            D d = default(D);
            if (t == null)
            {
               return default(D);
            }
            d = this.GetMappedType<T, D>(new List<T>() { t }).SingleOrDefault();
            return d;
        }

        private List<AddressType> AddressTypeMapper(List<CB_AddressType> source)
        {
            Mapper.CreateMap<CB_AddressType, AddressType>().ForMember(at => at.AddressTypeName, ca => ca.MapFrom(a => a.Address_TypeName));
            return Mapper.Map<List<AddressType>>(source);
        }

        private List<CB_AddressType> MdlAddressTypeToAddressType(List<AddressType> source)
        {
            Mapper.CreateMap<AddressType, CB_AddressType>().ForMember(m => m.Address_TypeName, ad => ad.MapFrom(a => a.AddressTypeName));
            return Mapper.Map<List<CB_AddressType>>(source);
        }

        private List<CB_NumberType> MdlNumberTypeToCBNumberType(List<NumberType> source)
        {
            Mapper.CreateMap<NumberType, CB_NumberType>()
    .ForMember(md => md.Number_TypeName, cb => cb.MapFrom(m => m.NumberTypeName));
            List<CB_NumberType> numberTypeList = Mapper.Map<List<CB_NumberType>>(source);
            return numberTypeList;
        }

        private List<NumberType> CBNumberTypeToMdlModelType(List<CB_NumberType> cbNumberType)
        {
            Mapper.CreateMap<CB_NumberType, NumberType>()
    .ForMember(md => md.NumberTypeName, cb => cb.MapFrom(m => m.Number_TypeName))
    .ForSourceMember(cb => cb.CB_ContactBook, c => c.Ignore())
    .ForSourceMember(cb => cb.CB_Numbers, c => c.Ignore());
            return Mapper.Map<List<NumberType>>(cbNumberType);
        }
    }

    [Serializable]
    public class ModelMapperException : Exception
    {
        public ModelMapperException() { }
        public ModelMapperException(string message) : base(message) { }
        public ModelMapperException(string message, Exception inner) : base(message, inner) { }
        protected ModelMapperException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
