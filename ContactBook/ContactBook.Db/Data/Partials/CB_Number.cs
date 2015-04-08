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
                && other.Number.Equals(this.Number)
                && other.NumberTypeId.Equals(this.NumberTypeId)
                && other.NumberTypeId.Equals(this.ContactId))
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


        public CB_Number Clone(object obj)
        {
            if (obj == null)
            {
                return default(CB_Number);
            }
            CB_Number actualNumber = obj as CB_Number;
            CB_Number cloneNumber = new CB_Number();
            cloneNumber.ContactId = actualNumber.ContactId;
            cloneNumber.Number = actualNumber.Number;
            cloneNumber.NumberId = actualNumber.NumberId;
            cloneNumber.NumberTypeId = actualNumber.NumberTypeId;
            return cloneNumber;
        }

    }
}
