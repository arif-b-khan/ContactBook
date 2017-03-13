using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ContactBook.Db.Data
{
    public partial class Address: IEquatable<Address>, INewEntity<Address>, IEntityCloneable<Address>
    {
        public static IEqualityComparer<Address> Comparer
        {
            get
            {
                return new CBAddressComparer();
            }
        }

        public class CBAddressComparer : IEqualityComparer<Address>
        {
            public bool Equals(Address x, Address y)
            {
                if (x.AddressId == y.AddressId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(Address obj)
            {
                return obj.AddressId.GetHashCode();
            }
        }

        public bool Equals(Address other)
        {
            if (other.AddressId.Equals(this.AddressId)
                && other.Address1 == this.Address1
                && other.AddressTypeId.Equals(this.AddressTypeId)
                && other.ContactId.Equals(this.ContactId))
            {
                return true;
            }
            return false;
        }

        public bool Equals(int id)
        {
            if (this.AddressId.Equals(id))
            {
                return true;
            }
            return false;
        }

        public Address Clone()
        {
            Address cloneAddress = new Address();
            cloneAddress.ContactId = this.ContactId;
            cloneAddress.Address1 = this.Address1;
            cloneAddress.AddressId = this.AddressId;
            cloneAddress.AddressTypeId = this.AddressTypeId;
            return cloneAddress;
        }

    }
}
