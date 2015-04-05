using AutoMapper;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContactBook.Domain.Contexts.Addresses
{
    public class AddressContext : ContactBook.Domain.Contexts.Addresses.IAddressContext
    {
        private IContactBookRepositoryUow unitOfWork;
        private IContactBookDbRepository<CB_Address> addressRepo;

        public AddressContext(IContactBookRepositoryUow unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            addressRepo = this.unitOfWork.GetEntityByType<CB_Address>();
        }

        public List<Address> GetAddressContactId(long contactId)
        {
            List<CB_Address> address = addressRepo.Get(ad => ad.ContactId == contactId, null, "CB_AddressType").ToList();
            Mapper.CreateMap<CB_Address, Address>();
            return Mapper.Map<List<Address>>(address);
        }

        public void InsertAddresses(List<Address> addresses)
        {
            foreach (var address in ReturnAddressFromModel(addresses))
            {
                addressRepo.Insert(address);
            }
            unitOfWork.Save();
        }

        public void UpdateAddresses(List<Address> addresses)
        {
            foreach (var address in ReturnAddressFromModel(addresses))
            {
                addressRepo.Update(address);
            }
            unitOfWork.Save();
        }

        public void DeleteAddresses(List<Address> addresses)
        {
            foreach (var address in ReturnAddressFromModel(addresses))
            {
                addressRepo.Delete(address);
            }
            unitOfWork.Save();
        }

        private List<CB_Address> ReturnAddressFromModel(List<Address> addresses)
        {
            Mapper.CreateMap<Address, CB_Address>();
            return Mapper.Map<List<CB_Address>>(addresses);
        }
    }
}