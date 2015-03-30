using AutoMapper;
using ContactBook.Db.Data;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Contexts.Address
{
    public class AddressContext : ContactBook.Domain.Contexts.Address.IAddressContext
    {
        IContactBookRepositoryUow unitOfWork;
        IContactBookDbRepository<CB_Address> addressRepo;
        
        public AddressContext(IContactBookRepositoryUow unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            addressRepo = this.unitOfWork.GetEntityByType<CB_Address>();
        }

        public List<MdlAddress> GetAddressContactId(long contactId)
        {
            List<CB_Address> address = addressRepo.Get(ad => ad.ContactId == contactId, null, "CB_AddressType").ToList();
            Mapper.CreateMap<CB_Address, MdlAddress>();
            return Mapper.Map<List<MdlAddress>>(address);
        }

        public void InsertAddresses(List<MdlAddress> addresses)
        {
            foreach(var address in ReturnAddressFromModel(addresses)){
                addressRepo.Insert(address);
            }
        }

        public void UpdateAddresses(List<MdlAddress> addresses)
        {
            foreach (var address in ReturnAddressFromModel(addresses))
            {
                addressRepo.Update(address);
            }
        }

        public void DeleteAddresses(List<MdlAddress> addresses)
        {
            foreach (var address in ReturnAddressFromModel(addresses))
            {
                addressRepo.Delete(address);
            }
        }

        private List<CB_Address> ReturnAddressFromModel(List<MdlAddress> addresses)
        {
            Mapper.CreateMap<MdlAddress, CB_Address>();
            return Mapper.Map<List<CB_Address>>(addresses);
        }
    }
}
