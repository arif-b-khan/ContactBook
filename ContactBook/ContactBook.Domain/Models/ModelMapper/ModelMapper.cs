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
                case "CB_AddressType-to-AddressType":
                    retListD = AddressTypeMapper(t as List<CB_AddressType>) as List<D>;
                    break;

                case "AddressType-to-CB_AddressType":
                    retListD = MdlAddressTypeToAddressType(t as List<AddressType>) as List<D>;
                    break;

                case "NumberType-to-CB_NumberType":
                    retListD = MdlNumberTypeToCBNumberType(t as List<NumberType>) as List<D>;
                    break;

                case "CB_NumberType-to-NumberType":
                    retListD = CBNumberTypeToMdlModelType(t as List<CB_NumberType>) as List<D>;
                    break;

                case "EmailType-to-CB_EmailType":
                    retListD = EmailTypeToCBEmailType(t as List<EmailType>) as List<D>;
                    break;

                case "CB_EmailType-to-EmailType":
                    retListD = CBEmailTypeToEmailType(t as List<CB_EmailType>) as List<D>;
                    break;

                case "IMType-to-CB_IMType":
                    retListD = IMTypeToCBIMType(t as List<IMType>) as List<D>;
                    break;

                case "CB_IMType-to-IMType":
                    retListD = CBIMTypeToIMType(t as List<CB_IMType>) as List<D>;
                    break;

                case "GroupType-to-CB_GroupType":
                    retListD = GroupTypeToCBGroupType(t as List<GroupType>) as List<D>;
                    break;

                case "CB_GroupType-to-GroupType":
                    retListD = CBGroupTypeToGroupType(t as List<CB_GroupType>) as List<D>;
                    break;

                case "RelationshipType-to-CB_RelationshipType":
                    retListD = RelationshipTypeToCBRelationshipType(t as List<RelationshipType>) as List<D>;
                    break;

                case "CB_RelationshipType-to-RelationshipType":
                    retListD = CBRelationshipTypeToRelationshipType(t as List<CB_RelationshipType>) as List<D>;
                    break;
                case "SpecialDateType-to-CB_SpecialDateType":
                    retListD = SpecialDateTypeToCBSpecialDate(t as List<SpecialDateType>) as List<D>;
                    break;
                case "CB_SpecialDateType-to-SpecialDateType":
                    retListD = CBSpecialDateTypeToSpecialDateType(t as List<CB_SpecialDateType>) as List<D>; 
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

        private List<CB_SpecialDateType> SpecialDateTypeToCBSpecialDate(List<SpecialDateType> source)
        {
            Mapper.CreateMap<SpecialDateType, CB_SpecialDateType>().ForMember(cb => cb.Date_TypeName, em => em.MapFrom(m => m.DateTypeName));
            return Mapper.Map<List<CB_SpecialDateType>>(source);
        }

        private List<SpecialDateType> CBSpecialDateTypeToSpecialDateType(List<CB_SpecialDateType> source)
        {
            Mapper.CreateMap<CB_SpecialDateType, SpecialDateType>().ForMember(em => em.DateTypeName, cb => cb.MapFrom(m => m.Date_TypeName));
            return Mapper.Map<List<SpecialDateType>>(source);
        }

        private List<CB_RelationshipType> RelationshipTypeToCBRelationshipType(List<RelationshipType> source)
        {
            Mapper.CreateMap<RelationshipType, CB_RelationshipType>().ForMember(cb => cb.Relationship_TypeName, em => em.MapFrom(m => m.RelationshipTypeName));
            return Mapper.Map<List<CB_RelationshipType>>(source);
        }

        private List<RelationshipType> CBRelationshipTypeToRelationshipType(List<CB_RelationshipType> source)
        {
            Mapper.CreateMap<CB_RelationshipType, RelationshipType>().ForMember(em => em.RelationshipTypeName, cb => cb.MapFrom(m => m.Relationship_TypeName));
            return Mapper.Map<List<RelationshipType>>(source);
        }

        private List<CB_GroupType> GroupTypeToCBGroupType(List<GroupType> source)
        {
            Mapper.CreateMap<GroupType, CB_GroupType>().ForMember(cb => cb.Group_TypeName, em => em.MapFrom(m => m.GroupTypeName));
            return Mapper.Map<List<CB_GroupType>>(source);
        }

        private List<GroupType> CBGroupTypeToGroupType(List<CB_GroupType> source)
        {
            Mapper.CreateMap<CB_GroupType, GroupType>().ForMember(em => em.GroupTypeName, cb => cb.MapFrom(m => m.Group_TypeName));
            return Mapper.Map<List<GroupType>>(source);
        }

        private List<IMType> CBIMTypeToIMType(List<CB_IMType> source)
        {
            Mapper.CreateMap<CB_IMType, IMType>().ForMember(em => em.IMTypeName, cb => cb.MapFrom(m => m.IM_TypeName)).ForSourceMember(s => s.IMLogoPath, sb => sb.Ignore());
            return Mapper.Map<List<IMType>>(source);
        }

        private List<CB_IMType> IMTypeToCBIMType(List<IMType> source)
        {
            Mapper.CreateMap<IMType, CB_IMType>().ForMember(cb => cb.IM_TypeName, em => em.MapFrom(m => m.IMTypeName));
            return Mapper.Map<List<CB_IMType>>(source);
        }

        private List<EmailType> CBEmailTypeToEmailType(List<CB_EmailType> source)
        {
            Mapper.CreateMap<CB_EmailType, EmailType>().ForMember(em => em.EmailTypeName, cb => cb.MapFrom(m => m.Email_TypeName));
            return Mapper.Map<List<EmailType>>(source);
        }

        private List<CB_EmailType> EmailTypeToCBEmailType(List<EmailType> source)
        {
            Mapper.CreateMap<EmailType, CB_EmailType>().ForMember(cb => cb.Email_TypeName, em => em.MapFrom(m => m.EmailTypeName));
            return Mapper.Map<List<CB_EmailType>>(source);
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