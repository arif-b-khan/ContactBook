using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class Number : IEquatable<Number>, INewEntity<Number>, IEntityCloneable<Number>
    {
        public static IEqualityComparer<Number> Comparer
        {
            get
            {
                return new CBNumberComparer();
            }
        }

        public class CBNumberComparer : IEqualityComparer<Number>
        {
            public CBNumberComparer()
            {

            }
            public bool Equals(Number x, Number y)
            {
                if (x.NumberId == y.NumberId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(Number obj)
            {
                return obj.NumberId.GetHashCode();
            }
        }

        public bool Equals(Number other)
        {
            if (other.NumberId.Equals(this.NumberId)
                && other.Number1 == this.Number1
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

        public Number Clone()
        {
            Number cloneNumber = new Number();
            cloneNumber.ContactId = this.ContactId;
            cloneNumber.Number1 = this.Number1;
            cloneNumber.NumberId = this.NumberId;
            cloneNumber.NumberTypeId = this.NumberTypeId;
            return cloneNumber;
        }

    }
}
