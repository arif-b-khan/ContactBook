using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts.Contacts.Helpers;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;

namespace ContactBook.Domain.Contexts.Contacts
{
    public class ContactContext : IContactContext
    {
        private const string CONTACT_FIELDS = "Addresses, Numbers, Emails, Websites, ContactByGroups, Addresses, IMs, Relationships, SpecialDates, InternetCalls";
        private IContactBookRepositoryUow unitOfWork;
        private IContactBookDbRepository<Contact> contactRepo;
        private ChildEntityDbOperations childOperations;

        public ContactContext(IContactBookRepositoryUow unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            contactRepo = this.unitOfWork.GetEntityByType<Contact>();
            childOperations = new ChildEntityDbOperations(contactRepo, unitOfWork);
            ContactModelToContactMapping();
            ContactToContactModelMapping();
        }

        public ContactModel GetContact(long contactId)
        {
            Contact contact = contactRepo.Get(con => con.ContactId == contactId, odr => odr.OrderBy(o => o.Firstname), CONTACT_FIELDS).FirstOrDefault();

            ContactModel retContact = Mapper.Map<Contact, ContactModel>(contact);

            return retContact;
        }

        public ContactModel GetContact(long bookId, long contactId)
        {
            Contact contact = contactRepo.Get(con => con.ContactId == contactId && con.BookId == bookId, odr => odr.OrderBy(o => o.Firstname), CONTACT_FIELDS).FirstOrDefault();

            ContactModel retContact = Mapper.Map<Contact, ContactModel>(contact);

            return retContact;
        
        }

        public List<ContactModel> GetContacts(long bookId)
        {
            List<Contact> contacts = contactRepo.Get(con => con.BookId == bookId, odr => odr.OrderBy(o => o.Firstname), CONTACT_FIELDS).ToList();
            List<ContactModel> contactList = Mapper.Map<List<ContactModel>>(contacts);
            return contactList;
        }

        public void InsertContact(ContactModel contact)
        {
            Contact cbContact = Mapper.Map<Contact>(contact);
            contactRepo.Insert(cbContact);
            unitOfWork.Save();
        }

        public void DeleteContact(ContactModel contact)
        {
            Contact cbContact = Mapper.Map<Contact>(contact);
            contactRepo.Delete(cbContact);
            unitOfWork.Save();
        }

        public void UpdateContact(ContactModel contact)
        {
            Contact modelContact = Mapper.Map<Contact>(contact);
            //Contact dContact = rContactRepo.GetById(contact.ContactId);
            Contact dContact = contactRepo.GetById(contact.ContactId);
            if (dContact != null && modelContact != null)
                childOperations.PerformOperations(modelContact, dContact);
            contactRepo.Update(dContact);
            unitOfWork.Save();
        }

        private static void ContactModelToContactMapping()
        {
            Mapper.CreateMap<ContactModel, Contact>()
                .ForMember(md => md.Addresses, cn => cn.MapFrom(m => m.Addresses))
                .ForMember(md => md.Numbers, cn => cn.MapFrom(m => m.Numbers))
                .ForMember(md => md.InternetCalls, cn => cn.MapFrom(m => m.InternetCalls))
                .ForMember(md => md.Websites, cn => cn.MapFrom(m => m.Websites))
                .ForMember(md => md.Relationships, cn => cn.MapFrom(m => m.Relationships))
                .ForMember(md => md.IMs, cn => cn.MapFrom(m => m.IMs))
                .ForMember(md => md.ContactByGroups, cn => cn.MapFrom(m => m.ContactByGroups))
                .ForMember(md => md.Emails, cn => cn.MapFrom(m => m.Emails))
                .ForMember(md => md.SpecialDates, cn => cn.MapFrom(m => m.SpecialDates));

            Mapper.CreateMap<AddressModel, Db.Data.Address>()
                .ForMember(cad => cad.Address1, (IMemberConfigurationExpression<AddressModel> ad) => ad.MapFrom(a => a.AddressDescription));

            Mapper.CreateMap<NumberModel, Number>().ForMember(cb => cb.Number1,
                nm => nm.MapFrom(n => n.ContactNumber));

            Mapper.CreateMap<IMModel, IM>();

            Mapper.CreateMap<InternetCallModel, InternetCall>();

            Mapper.CreateMap<WebsiteModel, Website>()
                .ForMember(cbw => cbw.Website1, wb => wb.MapFrom(w => w.WebsiteUrl));

            Mapper.CreateMap<RelationshipModel, Relationship>();

            Mapper.CreateMap<SpecialDateModel, SpecialDate>();

            Mapper.CreateMap<ContactByGroupModel, ContactByGroup>();

            Mapper.CreateMap<EmailModel, Email>()
                .ForMember(c => c.Email1, em => em.MapFrom(e => e.EmailAddress));

        }

        private static void ContactToContactModelMapping()
        {
            Mapper.CreateMap<Contact, ContactModel>()
                .ForSourceMember(cn => cn.ContactBook, cb => cb.Ignore())
                .ForMember(md => md.Addresses, cn => cn.MapFrom(m => m.Addresses))
                .ForMember(md => md.Numbers, cn => cn.MapFrom(m => m.Numbers))
                .ForMember(md => md.InternetCalls, cn => cn.MapFrom(m => m.InternetCalls))
                .ForMember(md => md.Websites, cn => cn.MapFrom(m => m.Websites))
                .ForMember(md => md.Relationships, cn => cn.MapFrom(m => m.Relationships))
                .ForMember(md => md.IMs, cn => cn.MapFrom(m => m.IMs))
                .ForMember(md => md.ContactByGroups, cn => cn.MapFrom(m => m.ContactByGroups))
                .ForMember(md => md.Emails, cn => cn.MapFrom(m => m.Emails))
                .ForMember(md => md.SpecialDates, cn => cn.MapFrom(m => m.SpecialDates));

            Mapper.CreateMap<Db.Data.Address, AddressModel>()
                .ForSourceMember(cb => cb.Contact, (ISourceMemberConfigurationExpression<Db.Data.Address> cbs) => cbs.Ignore())
                .ForSourceMember(cb => cb.AddressType, (ISourceMemberConfigurationExpression<Db.Data.Address> cbs) => cbs.Ignore())
                .ForMember(cad => cad.AddressDescription, (IMemberConfigurationExpression<Db.Data.Address> ad) => ad.MapFrom(a => a.Address1));

            Mapper.CreateMap<Number, NumberModel>()
                .ForSourceMember(cb => cb.Contact, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.NumberType, cbs => cbs.Ignore())
                .ForMember(n => n.ContactNumber, cb => cb.MapFrom(nm => nm.Number1));

            Mapper.CreateMap<IM, IM>()
                .ForSourceMember(cb => cb.Contact, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.IMType, cbs => cbs.Ignore());

            Mapper.CreateMap<InternetCall, InternetCall>()
                .ForSourceMember(cb => cb.Contact, cbs => cbs.Ignore());

            Mapper.CreateMap<Website, WebsiteModel>()
                .ForSourceMember(cb => cb.Contact, cbs => cbs.Ignore())
                .ForMember(cbw => cbw.WebsiteUrl, wb => wb.MapFrom(w => w.Website1));

            Mapper.CreateMap<Relationship, RelationshipModel>()
                .ForSourceMember(cb => cb.Contact, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.RelationshipType, cbs => cbs.Ignore());

            Mapper.CreateMap<SpecialDate, SpecialDateModel>()
                .ForSourceMember(cb => cb.Contact, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.SpecialDateType, cbs => cbs.Ignore());

            Mapper.CreateMap<ContactByGroup, ContactByGroupModel>()
                .ForSourceMember(cb => cb.Contact, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.GroupType, cbs => cbs.Ignore());

            Mapper.CreateMap<Email, EmailModel>()
                .ForSourceMember(cb => cb.Contact, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.EmailType, cbs => cbs.Ignore())
                .ForMember(c => c.EmailAddress, em => em.MapFrom(e => e.Email1));

            Mapper.CreateMap<AddressType, AddressTypeModel>()
                .ForSourceMember(cb => cb.Addresses, cbs => cbs.Ignore())
                .ForSourceMember(cb => cb.ContactBook, cbs => cbs.Ignore());
        }
    }
}