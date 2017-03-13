using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class Website : IEquatable<Website>, INewEntity<Website>, IEntityCloneable<Website>
    {
        public static IEqualityComparer<Website> Comparer
        {
            get
            {
                return new CBWebsiteComparer();
            }
        }

        public class CBWebsiteComparer : IEqualityComparer<Website>
        {
            public CBWebsiteComparer()
            {
            }

            public bool Equals(Website x, Website y)
            {
                if (x.WebsiteId == y.WebsiteId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(Website obj)
            {
                return obj.WebsiteId.GetHashCode();
            }
        }

        public bool Equals(Website other)
        {
            if (other.WebsiteId.Equals(this.WebsiteId)
                && other.Website1 == this.Website1
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

        public Website Clone()
        {
            Website cloneWebsite = new Website();
            cloneWebsite.ContactId = this.ContactId;
            cloneWebsite.Website1 = this.Website1;
            cloneWebsite.WebsiteId = this.WebsiteId;
            return cloneWebsite;
        }
    }
}