using AutoMapper;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Contexts.Contact
{
    public class ContactContext : IContactContext
    {
        IContactBookRepositoryUow unitOfWork;
        IContactBookDbRepository<CB_Contact> addressRepo;

        public ContactContext(IContactBookRepositoryUow unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            addressRepo = this.unitOfWork.GetEntityByType<CB_Contact>();
        }

        //public List<MdlContact> GetContacts(long bookId)
        //{
        //    List<CB_Contact> contacts = addressRepo.Get(con => con.BookId == bookId, odr => odr.OrderBy(o => o.Firstname), "").ToList();

        //}

        public MdlContact GetContact(long contactId)
        {
            CB_Contact contact = addressRepo.Get(con => con.ContactId == contactId, odr => odr.OrderBy(o => o.Firstname), "CB_Addresses, CB_Numbers").FirstOrDefault();

            Mapper.CreateMap<CB_Contact, MdlContact>()
                .ForSourceMember(cn => cn.CB_ContactBook, cb => cb.Ignore())
                .ForMember(md => md.Addresses, cn => cn.MapFrom(m => m.CB_Addresses))
                .ForMember(md => md.Numbers, cn => cn.MapFrom(m => m.CB_Numbers))
                .ForMember(md => md.InternetCalls, cn => cn.MapFrom(m => m.CB_InternetCalls))
                .ForMember(md => md.Websites, cn => cn.MapFrom(m => m.CB_Websites))
                .ForMember(md => md.Relationships, cn => cn.MapFrom(m => m.CB_Relationships))
                .ForMember(md => md.IMs, cn => cn.MapFrom(m => m.CB_IMs))
                .ForMember(md => md.ContactByGroups, cn => cn.MapFrom(m => m.CB_ContactByGroups))
                .ForMember(md => md.Emails, cn => cn.MapFrom(m => m.CB_Emails))
                .ForMember(md => md.SpecialDates, cn => cn.MapFrom(m => m.CB_SpecialDates));

            Mapper.CreateMap<CB_Address, MdlAddress>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_AddressType, cbs => cbs.Ignore());
            
            Mapper.CreateMap<CB_Number, MdlNumber>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_NumberType, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_IM, MdlIM>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_IMType, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_InternetCall, MdlInternetCall>()
                .ForSourceMember(cb => cb.CB_Contacts,cbs => cbs.Ignore());
            
            Mapper.CreateMap<CB_Website, MdlWebsite>()
                .ForSourceMember(cb => cb.CB_Contact, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_Relationship, MdlRelationship>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_RelationshipType, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_SpecialDates, MdlSpecialDates>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_SpecialDateType, cbs => cbs.Ignore());
            
            Mapper.CreateMap<CB_ContactByGroup, MdlContactByGroup>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_GroupTypes, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_Email, MdlEmail>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_EmailType, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_AddressType, MdlAddressType>()
                .ForSourceMember(cb => cb.CB_Address, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_ContactBook, cbs => cbs.Ignore());

            MdlContact retContact = Mapper.Map<CB_Contact, MdlContact>(contact);
            
            return retContact;
        }


    }
}
