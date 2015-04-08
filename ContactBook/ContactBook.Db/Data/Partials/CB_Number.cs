using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class CB_Number : IEquatable<CB_Number>, INewEntity<CB_Number>, IEntityCloneable<CB_Number>
    {
        public static IEqualityComparer<CB_Number> Comparer
        {
            get
            {
                return new CBNumberComparer();
            }
        }

        public class CBNumberComparer : IEqualityComparer<CB_Number>
        {
            public CBNumberComparer()
            {

            }
            public bool Equals(CB_Number x, CB_Number y)
            {
                if (x.NumberId == y.NumberId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(CB_Number obj)
            {
                return obj.NumberId.GetHashCode();
            }
        }

        public bool Equals(CB_Number other)
        {
            if (other.NumberId.Equals(this.NumberId)
                && other.Number == this.Number
                && other.NumberTypeId.Equals(this.NumberTypeId)
                && other.ContactId.Equals(this.ContactId))
            {
                return true;
            }
            return false;
        }

        public bool Equals(int id)
        {
            if (this.NumberId.Equals(id))
            {
                return true;
            }
            return false;
        }

        public CB_Number Clone()
        {
            CB_Number cloneNumber = new CB_Number();
            cloneNumber.ContactId = this.ContactId;
            cloneNumber.Number = this.Number;
            cloneNumber.NumberId = this.NumberId;
            cloneNumber.NumberTypeId = this.NumberTypeId;
            return cloneNumber;
        }

    }
}
