using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class CB_SpecialDate : IEquatable<CB_SpecialDate>, INewEntity<CB_SpecialDate>, IEntityCloneable<CB_SpecialDate>
    {
        public static IEqualityComparer<CB_SpecialDate> Comparer
        {
            get
            {
                return new CBSpecialDateComparer();
            }
        }

        public class CBSpecialDateComparer : IEqualityComparer<CB_SpecialDate>
        {
            public CBSpecialDateComparer()
            {

            }
            public bool Equals(CB_SpecialDate x, CB_SpecialDate y)
            {
                if (x.SpecialDateId == y.SpecialDateId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(CB_SpecialDate obj)
            {
                return obj.SpecialDateId.GetHashCode();
            }
        }

        public bool Equals(CB_SpecialDate other)
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

        public CB_SpecialDate Clone()
        {
            CB_SpecialDate cloneSpecialDate = new CB_SpecialDate();
            cloneSpecialDate.ContactId = this.ContactId;
            cloneSpecialDate.Dates = this.Dates;
            cloneSpecialDate.SpecialDateId = this.SpecialDateId;
            cloneSpecialDate.SpecialDateTpId = this.SpecialDateTpId;
            return cloneSpecialDate;
        }
    }

}
