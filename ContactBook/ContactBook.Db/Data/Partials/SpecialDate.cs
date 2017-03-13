using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class SpecialDate : IEquatable<SpecialDate>, INewEntity<SpecialDate>, IEntityCloneable<SpecialDate>
    {
        public static IEqualityComparer<SpecialDate> Comparer
        {
            get
            {
                return new CBSpecialDateComparer();
            }
        }

        public class CBSpecialDateComparer : IEqualityComparer<SpecialDate>
        {
            public CBSpecialDateComparer()
            {

            }
            public bool Equals(SpecialDate x, SpecialDate y)
            {
                if (x.SpecialDateId == y.SpecialDateId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(SpecialDate obj)
            {
                return obj.SpecialDateId.GetHashCode();
            }
        }

        public bool Equals(SpecialDate other)
        {
            if (other.SpecialDateId.Equals(this.SpecialDateId)
                && other.Dates.ToShortDateString() == this.Dates.ToShortDateString()
                && other.SpecialDateTpId.Equals(this.SpecialDateTpId)
                && other.ContactId.Equals(this.ContactId))
            {
                return true;
            }
            return false;
        }

        public bool Equals(int id)
        {
            if (this.SpecialDateId.Equals(id))
            {
                return true;
            }
            return false;
        }

        public SpecialDate Clone()
        {
            SpecialDate cloneSpecialDate = new SpecialDate();
            cloneSpecialDate.ContactId = this.ContactId;
            cloneSpecialDate.Dates = this.Dates;
            cloneSpecialDate.SpecialDateId = this.SpecialDateId;
            cloneSpecialDate.SpecialDateTpId = this.SpecialDateTpId;
            return cloneSpecialDate;
        }
    }

}
