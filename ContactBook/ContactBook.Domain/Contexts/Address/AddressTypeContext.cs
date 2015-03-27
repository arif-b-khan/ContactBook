using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ContactBook.Domain.Models;
using ContactBook.Db.Repositories;
using ContactBook.Db.Data;
using AutoMapper;

namespace ContactBook.Domain.Contexts.Address
{
    public class AddressTypeContext : IAddressTypeContext
    {
        IContactBookRepositoryUow unitOfwork;
        IContactBookDbRepository<CB_AddressType> addressTypeRepo;

        public AddressTypeContext(IContactBookRepositoryUow unitOfwork)
        {
            this.unitOfwork = unitOfwork;
            this.addressTypeRepo = unitOfwork.GetEntityByType<CB_AddressType>();
        }

        public List<MdlAddressType> GetAddessType(long bookId)
        {
            List<MdlAddressType> modelAddrTypes;
            var addressTypes = addressTypeRepo.Get(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue));
            Mapper.CreateMap<CB_AddressType, MdlAddressType>();
            modelAddrTypes = Mapper.Map<List<MdlAddressType>>(addressTypes);
            return modelAddrTypes;
        }

        public void AddAddressTypes(List<MdlAddressType> addressType)
        {
            foreach (var address in GetCBAddressTypeList(addressType))
            {
                addressTypeRepo.Insert(address);
            }
            unitOfwork.Save();
        }

        public void RemoveAddressTypes(List<MdlAddressType> addressTypes)
        {
            foreach (var address in GetCBAddressTypeList(addressTypes))
            {
                addressTypeRepo.Delete(address);
            }
            unitOfwork.Save();
        }

        public void UpdateAddressTypes(List<MdlAddressType> addressTypes)
        {
            foreach (var address in GetCBAddressTypeList(addressTypes))
            {
                addressTypeRepo.Update(address);
            }
            unitOfwork.Save();
        }

        private List<CB_AddressType> GetCBAddressTypeList(List<MdlAddressType> addressTypes)
        {
            Mapper.CreateMap<MdlAddressType, CB_AddressType>();
            return Mapper.Map<List<CB_AddressType>>(addressTypes);
        }

    }
}
