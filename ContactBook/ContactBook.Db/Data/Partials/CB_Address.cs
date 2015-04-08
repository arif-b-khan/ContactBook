using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ContactBook.Db.Data
{
    public partial class CB_Address: IEquatable<CB_Address>, INewEntity<CB_Address>, IEntityCloneable<CB_Address>
    {
        public static IEqualityComparer<CB_Address> Comparer
        {
            get
            {
                return new CBAddressComparer();
            }
        }

        public class CBAddressComparer : IEqualityComparer<CB_Address>
        {
            public bool Equals(CB_Address x, CB_Address y)
            {
                if (x.AddressId == y.AddressId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(CB_Address obj)
            {
                return obj.AddressId.GetHashCode();
            }
        }

        public bool Equals(CB_Address other)
        {
            if (other.AddressId.Equals(this.AddressId)
                && other.Address == this.Address
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

        public CB_Address Clone()
        {
            CB_Address cloneAddress = new CB_Address();
            cloneAddress.ContactId = this.ContactId;
            cloneAddress.Address = this.Address;
            cloneAddress.AddressId = this.AddressId;
            cloneAddress.AddressTypeId = this.AddressTypeId;
            return cloneAddress;
        }

    }
}
