using AutoMapper;
using ContactBook.Db.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactBook.Domain.Models.ModelMapper
{
    public class ModelMapper
    {
        public List<D> GetMappedType<T, D>(List<T> t)
        {
            string convertType = typeof(T).Name + "-to-" + typeof(D).Name;
            List<D> retListD = null;
            switch (convertType)
            {
                case "AddressTypeModel-to-AddressType":
                    retListD = AddressTypeModelToAddressType(t as List<AddressTypeModel>) as List<D>;
                    break;

                case "AddressType-to-AddressTypeModel":
                    retListD = AddressTypeToAddressTypeModel(t as List<AddressType>) as List<D>;
                    break;

                case "NumberTypeModel-to-NumberType":
                    retListD = NumberTypeModelToNumberType(t as List<NumberTypeModel>) as List<D>;
                    break;

                case "NumberType-to-NumberTypeModel":
                    retListD = NumberTypeToNumberTypeModel(t as List<NumberType>) as List<D>;
                    break;

                case "EmailTypeModel-to-EmailType":
                    retListD = EmailTypeModelToEmailType(t as List<EmailTypeModel>) as List<D>;
                    break;

                case "EmailType-to-EmailTypeModel":
                    retListD = EmailTypeToEmailTypeModel(t as List<EmailType>) as List<D>;
                    break;

                case "IMTypeModel-to-IMType":
                    retListD = IMTypeModelToIMType(t as List<IMTypeModel>) as List<D>;
                    break;

                case "IMType-to-IMTypeModel":
                    retListD = IMTypeToIMTypeModel(t as List<IMType>) as List<D>;
                    break;

                case "GroupType-to-GroupTypeModel":
                    retListD = GroupTypeToGroupTypeModel(t as List<GroupType>) as List<D>;
                    break;

                case "GroupTypeModel-to-GroupType":
                    retListD = GroupTypeModelToGroupType(t as List<GroupTypeModel>) as List<D>;
                    break;

                case "RelationshipType-to-RelationshipTypeModel":
                    retListD = RelationshipTypeToRelationshipTypeModel(t as List<RelationshipType>) as List<D>;
                    break;

                case "RelationshipTypeModel-to-RelationshipType":
                    retListD = RelationshipTypeModelToRelationshipType(t as List<RelationshipTypeModel>) as List<D>; 
                    break;
                case "SpecialDateType-to-SpecialDateTypeModel":
                    retListD = SpecialDateTypeToSpecialDateTypeModel(t as List<SpecialDateType>) as List<D>;
                    break;
                case "SpecialDateTypeModel-to-SpecialDateType":
                    retListD = SpecialDateTypeModelToSpecialDateType(t as List<SpecialDateTypeModel>) as List<D>;
                    break;
                default:
                    throw new ModelMapperException(string.Format("Mapping method not available.\nPlease add mapping method to convert the type from {0} to {1}", typeof(T).Name, typeof(D).Name));
            }
            if (retListD == null)
            {
                return default(List<D>);
            }
            else
            {
                return retListD;
            }
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

        private List<SpecialDateType> SpecialDateTypeModelToSpecialDateType(List<SpecialDateTypeModel> source)
        {
            Mapper.CreateMap<SpecialDateTypeModel, SpecialDateType>().ForMember(cb => cb.Date_TypeName, em => em.MapFrom(m => m.DateTypeName));
            return Mapper.Map<List<SpecialDateType>>(source);
        }

        private List<SpecialDateTypeModel> SpecialDateTypeToSpecialDateTypeModel(List<SpecialDateType> source)
        {
            Mapper.CreateMap<SpecialDateType, SpecialDateTypeModel>().ForMember(em => em.DateTypeName, cb => cb.MapFrom(m => m.Date_TypeName));
            return Mapper.Map<List<SpecialDateTypeModel>>(source);
        }

        private List<RelationshipType> RelationshipTypeModelToRelationshipType(List<RelationshipTypeModel> source)
        {
            Mapper.CreateMap<RelationshipTypeModel, RelationshipType>().ForMember(cb => cb.Relationship_TypeName, em => em.MapFrom(m => m.RelationshipTypeName));
            return Mapper.Map<List<RelationshipType>>(source);
        }

        private List<RelationshipTypeModel> RelationshipTypeToRelationshipTypeModel(List<RelationshipType> source)
        {
            Mapper.CreateMap<RelationshipType, RelationshipTypeModel>().ForMember(em => em.RelationshipTypeName, cb => cb.MapFrom(m => m.Relationship_TypeName));
            return Mapper.Map<List<RelationshipTypeModel>>(source);
        }

        private List<GroupType> GroupTypeModelToGroupType(List<GroupTypeModel> source)
        {
            Mapper.CreateMap<GroupTypeModel, GroupType>().ForMember(cb => cb.Group_TypeName, em => em.MapFrom(m => m.GroupTypeName));
            return Mapper.Map<List<GroupType>>(source);
        }

        private List<GroupTypeModel> GroupTypeToGroupTypeModel(List<GroupType> source)
        {
            Mapper.CreateMap<GroupType, GroupTypeModel>().ForMember(em => em.GroupTypeName, cb => cb.MapFrom(m => m.Group_TypeName));
            return Mapper.Map<List<GroupTypeModel>>(source);
        }

        private List<IMTypeModel> IMTypeToIMTypeModel(List<IMType> source)
        {
            Mapper.CreateMap<IMType, IMTypeModel>().ForMember(em => em.IMTypeName, cb => cb.MapFrom(m => m.IM_TypeName)).ForSourceMember(s => s.IMLogoPath, sb => sb.Ignore());
            return Mapper.Map<List<IMTypeModel>>(source);
        }

        private List<IMType> IMTypeModelToIMType(List<IMTypeModel> source)
        {
            Mapper.CreateMap<IMTypeModel, IMType>().ForMember(cb => cb.IM_TypeName, em => em.MapFrom(m => m.IMTypeName));
            return Mapper.Map<List<IMType>>(source);
        }

        private List<EmailTypeModel> EmailTypeToEmailTypeModel(List<EmailType> source)
        {
            Mapper.CreateMap<EmailType, EmailTypeModel>().ForMember(em => em.EmailTypeName, cb => cb.MapFrom(m => m.Email_TypeName));
            return Mapper.Map<List<EmailTypeModel>>(source);
        }

        private List<EmailType> EmailTypeModelToEmailType(List<EmailTypeModel> source)
        {
            Mapper.CreateMap<EmailTypeModel, EmailType>().ForMember(cb => cb.Email_TypeName, em => em.MapFrom(m => m.EmailTypeName));
            return Mapper.Map<List<EmailType>>(source);
        }

        private List<AddressType> AddressTypeModelToAddressType(List<AddressTypeModel> source)
        {
            Mapper.CreateMap<AddressTypeModel, AddressType>().ForMember(at => at.Address_TypeName, ca => ca.MapFrom(a => a.AddressTypeName));
            return Mapper.Map<List<AddressType>>(source);
        }

        private List<AddressTypeModel> AddressTypeToAddressTypeModel(List<AddressType> source)
        {
            Mapper.CreateMap<AddressType, AddressTypeModel>().ForMember(m => m.AddressTypeName, ad => ad.MapFrom(a => a.Address_TypeName));
            return Mapper.Map<List<AddressTypeModel>>(source);
        }

        private List<NumberType> NumberTypeModelToNumberType(List<NumberTypeModel> source)
        {
            Mapper.CreateMap<NumberTypeModel, NumberType>()
    .ForMember(md => md.Number_TypeName, cb => cb.MapFrom(m => m.NumberTypeName));
            List<NumberType> numberTypeList = Mapper.Map<List<NumberType>>(source);
            return numberTypeList;
        }

        private List<NumberTypeModel> NumberTypeToNumberTypeModel(List<NumberType> cbNumberType)
        {
            Mapper.CreateMap<NumberType, NumberTypeModel>()
    .ForMember(md => md.NumberTypeName, cb => cb.MapFrom(m => m.Number_TypeName))
    .ForSourceMember(cb => cb.ContactBook, c => c.Ignore())
    .ForSourceMember(cb => cb.Numbers, c => c.Ignore());
            return Mapper.Map<List<NumberTypeModel>>(cbNumberType);
        }
    }

    [Serializable]
    public class ModelMapperException : Exception
    {
        public ModelMapperException()
        {
        }

        public ModelMapperException(string message)
            : base(message)
        {
        }

        public ModelMapperException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ModelMapperException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}