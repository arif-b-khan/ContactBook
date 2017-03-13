using System;
using System.Collections.Generic;
using AutoMapper;

namespace ContactBook.Db.Data
{
    public partial class Email : IEquatable<Email>, INewEntity<Email>, IEntityCloneable<Email>
    {
        
        public class CB_EmailComparer : IEqualityComparer<Email>
        {
            
            public bool Equals(Email x, Email y)
            {
                if (x.EmailId.Equals(y.EmailId))
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(Email obj)
            {
                return obj.EmailId.GetHashCode();
            }
        }

        public static IEqualityComparer<Email> Comparer
        {
            get
            {
                return new CB_EmailComparer();
            }
        }

        public bool Equals(Email other)
        {
            if (other.EmailId.Equals(this.EmailId)
                && other.ContactId.Equals(this.ContactId)
                && other.Email1 == this.Email1
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

        public Email Clone()
        {
            Email retObj = new Email() {
            ContactId = this.ContactId,
            Email1 = this.Email1,
            EmailId = this.EmailId,
            EmailTypeId = this.EmailTypeId
            };
            return retObj;
        }
    }
}