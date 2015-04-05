using AutoMapper;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContactBook.Domain.Contexts.Contacts
{
    public class ContactContext : IContactContext
    {
        private IContactBookRepositoryUow unitOfWork;
        private IContactBookDbRepository<CB_Contact> contactRepo;
        private IContactBookDbRepository<CB_Number> numberRepo;

        public ContactContext(IContactBookRepositoryUow unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            contactRepo = this.unitOfWork.GetEntityByType<CB_Contact>();
            numberRepo = this.unitOfWork.GetEntityByType<CB_Number>();
            CBContactToContactMapping();
            ContactToCBContactMapping();
        }

        public Contact GetContact(long contactId)
        {
            CB_Contact contact = contactRepo.Get(con => con.ContactId == contactId, odr => odr.OrderBy(o => o.Firstname), "CB_Addresses, CB_Numbers").FirstOrDefault();

            CBContactToContactMapping();

            Contact retContact = Mapper.Map<CB_Contact, Contact>(contact);

            return retContact;
        }

        public List<Contact> GetContacts(long bookId)
        {
            List<CB_Contact> contacts = contactRepo.Get(con => con.BookId == bookId, odr => odr.OrderBy(o => o.Firstname), "").ToList();
            CBContactToContactMapping();
            List<Contact> contactList = Mapper.Map<List<Contact>>(contacts);
            return contactList;
        }

        public void InsertContact(Contact contact)
        {
            CB_Contact cbContact = Mapper.Map<CB_Contact>(contact);
            //cbContact.CB_Numbers.First().CB_Contacts = cbContact;
            contactRepo.Insert(cbContact);
            unitOfWork.Save();
        }
        public void DeleteContact(Contact contact)
        {
            CB_Contact cbContact = Mapper.Map<CB_Contact>(contact);
            cbContact.CB_Numbers.First().CB_Contacts = cbContact;
            contactRepo.Delete(cbContact);
            unitOfWork.Save();
        }

        public void UpdateContact(Contact contact)
        {
            CB_Contact cbContact = Mapper.Map<CB_Contact>(contact);
            contactRepo.Update(cbContact);
            unitOfWork.Save();
        }

        private static void ContactToCBContactMapping()
        {
            Mapper.CreateMap<Contact, CB_Contact>()
                .ForMember(md => md.CB_Addresses, cn => cn.MapFrom(m => m.Addresses))
                .ForMember(md => md.CB_Numbers, cn => cn.MapFrom(m => m.Numbers))
                .ForMember(md => md.CB_InternetCalls, cn => cn.MapFrom(m => m.InternetCalls))
                .ForMember(md => md.CB_Websites, cn => cn.MapFrom(m => m.Websites))
                .ForMember(md => md.CB_Relationships, cn => cn.MapFrom(m => m.Relationships))
                .ForMember(md => md.CB_IMs, cn => cn.MapFrom(m => m.IMs))
                .ForMember(md => md.CB_ContactByGroups, cn => cn.MapFrom(m => m.ContactByGroups))
                .ForMember(md => md.CB_Emails, cn => cn.MapFrom(m => m.Emails))
                .ForMember(md => md.CB_SpecialDates, cn => cn.MapFrom(m => m.SpecialDates));

            Mapper.CreateMap<Address, CB_Address>();

            Mapper.CreateMap<Number, CB_Number>().ForMember(cb => cb.Number,
                nm => nm.MapFrom(n => n.ContactNumber));
            Mapper.CreateMap<IM, CB_IM>();

            Mapper.CreateMap<InternetCall, CB_InternetCall>();

            Mapper.CreateMap<Website, CB_Website>();

            Mapper.CreateMap<Relationship, CB_Relationship>();

            Mapper.CreateMap<SpecialDates, CB_SpecialDates>();

            Mapper.CreateMap<ContactByGroup, CB_ContactByGroup>();

            Mapper.CreateMap<Email, CB_Email>();

            Mapper.CreateMap<AddressType, CB_AddressType>();
      
        }

        private static void CBContactToContactMapping()
        {
            Mapper.CreateMap<CB_Contact, Contact>()
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

            Mapper.CreateMap<CB_Address, Address>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_AddressType, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_Number, Number>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_NumberType, cbs => cbs.Ignore())
                .ForMember(n => n.ContactNumber, cb => cb.MapFrom(nm => nm.Number));

            Mapper.CreateMap<CB_IM, IM>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_IMType, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_InternetCall, InternetCall>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_Website, Website>()
                .ForSourceMember(cb => cb.CB_Contact, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_Relationship, Relationship>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_RelationshipType, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_SpecialDates, SpecialDates>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_SpecialDateType, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_ContactByGroup, ContactByGroup>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_GroupTypes, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_Email, Email>()
                .ForSourceMember(cb => cb.CB_Contacts, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_EmailType, cbs => cbs.Ignore());

            Mapper.CreateMap<CB_AddressType, AddressType>()
                .ForSourceMember(cb => cb.CB_Address, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.CB_ContactBook, cbs => cbs.Ignore());
        }
    }
}