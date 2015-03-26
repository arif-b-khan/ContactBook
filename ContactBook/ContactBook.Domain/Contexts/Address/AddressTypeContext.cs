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
    public class AddressTypeContext : ContactBaseContext, IUnitOfWork, ContactBook.Domain.Contexts.Address.IAddressTypeContext
    {
        public AddressTypeContext() : this("") { }

        public AddressTypeContext(string connection)
            : base(connection)
        {
            UnitOfWork = new ContactBookRepositoryUow(GetContainer);
        }


        public IContactBookRepositoryUow UnitOfWork
        {
            get;
            set;
        }

        public List<MdlAddressType> GetAddessType(long bookId)
        {
            if (UnitOfWork == null)
            {
                new NullReferenceException("UnitOfWork is cannot be null");
            }

            ContactBookDbRepository<CB_AddressType> addressRepo = UnitOfWork.GetEntityByType<CB_AddressType>();
            var addressTypes = addressRepo.Get(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue));
            Mapper.CreateMap<CB_AddressType, MdlAddressType>();
            List<MdlAddressType> modelAddrTypes = Mapper.Map<List<MdlAddressType>>(addressTypes);

            return modelAddrTypes;
        }

        public void AddAddressTypes(List<MdlAddressType> addressType)
        {
            if (UnitOfWork == null)
            {
                new NullReferenceException("UnitOfWork is cannot be null");
            }

            ContactBookDbRepository<CB_AddressType> addressRepo = UnitOfWork.GetEntityByType<CB_AddressType>();
            
            Mapper.CreateMap<List<MdlAddressType>, List<CB_AddressType>>();
            List<CB_AddressType> cbAddressTypes = Mapper.Map <List<CB_AddressType>>(addressType);
            
            foreach (var address in cbAddressTypes)
            {
                addressRepo.Insert(address);
            }

            UnitOfWork.Save();
        }

        protected override void Dispose(bool managedDispose)
        {
            UnitOfWork.Dispose();
            base.Dispose(managedDispose);
        }

    }
}
