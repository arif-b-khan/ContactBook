using AutoMapper;
using data = ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContactBook.Domain.Contexts.Addresses
{
    public class AddressContext : ContactBook.Domain.Contexts.Addresses.IAddressContext
    {
        private IContactBookRepositoryUow unitOfWork;
        private IContactBookDbRepository<Db.Data.Address> addressRepo;

        public AddressContext(IContactBookRepositoryUow unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            addressRepo = this.unitOfWork.GetEntityByType<Db.Data.Address>();
        }

        public List<AddressModel> GetAddressContactId(long contactId)
        {
            List<Db.Data.Address> address = addressRepo.Get(ad => ad.ContactId == contactId, null, "AddressType").ToList();
            Mapper.CreateMap<Db.Data.Address, AddressModel>();
            return Mapper.Map<List<AddressModel>>(address);
        }

        public void InsertAddresses(List<AddressModel> addresses)
        {
            foreach (var address in ReturnAddressFromModel(addresses))
            {
                addressRepo.Insert(address);
            }
            unitOfWork.Save();
        }

        public void UpdateAddresses(List<AddressModel> addresses)
        {
            foreach (var address in ReturnAddressFromModel(addresses))
            {
                addressRepo.Update(address);
            }
            unitOfWork.Save();
        }

        public void DeleteAddresses(List<AddressModel> addresses)
        {
            foreach (var address in ReturnAddressFromModel(addresses))
            {
                addressRepo.Delete(address);
            }
            unitOfWork.Save();
        }

        private List<Db.Data.Address> ReturnAddressFromModel(List<AddressModel> addresses)
        {
            Mapper.CreateMap<AddressModel, Db.Data.Address>();
            return Mapper.Map<List<Db.Data.Address>>(addresses);
        }
    }
}