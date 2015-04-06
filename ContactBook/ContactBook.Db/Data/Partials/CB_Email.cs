using System;
using System.Collections.Generic;

namespace ContactBook.Db.Data
{
    public partial class CB_Email : IEquatable<CB_Email>, INewEntity<CB_Email>
    {
        static Lazy<IEqualityComparer<CB_Email>> comparer = new Lazy<IEqualityComparer<CB_Email>>(true);

        private class CB_EmailComparer : IEqualityComparer<CB_Email>
        {
            public bool Equals(CB_Email x, CB_Email y)
            {
                if (x.EmailId.Equals(y.EmailId))
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(CB_Email obj)
            {
                return obj.EmailId.GetHashCode();
            }
        }

        public static IEqualityComparer<CB_Email> Comparer
        {
            get
            {
                return comparer.Value;
            }
        }

        public bool Equals(CB_Email other)
        {
            if (other.EmailId.Equals(this.EmailId)
                && other.ContactId.Equals(this.ContactId)
                && other.Email.Equals(this.Email)
                && other.EmailTypeId.Equals(this.EmailTypeId))
            {
                return true;
            }
            return false;
        }

        public bool Equals(int id)
        {
            if (this.EmailId.Equals(id))
            {
                return true;
            }
            return false;
        }

    }
}