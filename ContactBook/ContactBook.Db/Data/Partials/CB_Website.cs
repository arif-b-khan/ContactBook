using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class CB_Website : IEquatable<CB_Website>, INewEntity<CB_Website>, IEntityCloneable<CB_Website>
    {
        public static IEqualityComparer<CB_Website> Comparer
        {
            get
            {
                return new CBWebsiteComparer();
            }
        }

        public class CBWebsiteComparer : IEqualityComparer<CB_Website>
        {
            public CBWebsiteComparer()
            {
            }

            public bool Equals(CB_Website x, CB_Website y)
            {
                if (x.WebsiteId == y.WebsiteId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(CB_Website obj)
            {
                return obj.WebsiteId.GetHashCode();
            }
        }

        public bool Equals(CB_Website other)
        {
            if (other.WebsiteId.Equals(this.WebsiteId)
                && other.Website == this.Website
                && other.ContactId.Equals(this.ContactId))
            {
                return true;
            }
            return false;
        }

        public bool Equals(int id)
        {
            if (this.WebsiteId.Equals(id))
            {
                return true;
            }
            return false;
        }

        public CB_Website Clone()
        {
            CB_Website cloneWebsite = new CB_Website();
            cloneWebsite.ContactId = this.ContactId;
            cloneWebsite.Website = this.Website;
            cloneWebsite.WebsiteId = this.WebsiteId;
            return cloneWebsite;
        }
    }
}