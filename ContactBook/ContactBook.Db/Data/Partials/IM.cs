using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class IM : IEquatable<IM>, INewEntity<IM>, IEntityCloneable<IM>
    {
        public static IEqualityComparer<IM> Comparer
        {
            get
            {
                return new CBIMComparer();
            }
        }

        public class CBIMComparer : IEqualityComparer<IM>
        {
            public bool Equals(IM x, IM y)
            {
                if (x.IMId.Equals(y.IMId))
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(IM obj)
            {
                return obj.IMId.GetHashCode();
            }
        }

        public bool Equals(int id)
        {
            if (this.IMId.Equals(id))
            {
                return true;
            }
            return false;
        }

        public bool Equals(IM other)
        {
            if (other.IMId.Equals(this.IMId) 
                && other.IMTypeId.Equals(this.IMTypeId) 
                && other.Username == this.Username
                && other.ContactId.Equals(this.ContactId))
            {
                return true;
            }
            return false;
        }

        public IM Clone()
        {
            return new IM() { 
            ContactId = this.ContactId,
            IMId = this.IMId,
            IMTypeId = this.IMTypeId,
            Username = this.Username
            };
        }
    }
}
