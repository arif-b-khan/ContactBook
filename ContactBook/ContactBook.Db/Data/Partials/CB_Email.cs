using System;
using System.Collections.Generic;
using AutoMapper;

namespace ContactBook.Db.Data
{
    public partial class CB_Email : IEquatable<CB_Email>, INewEntity<CB_Email>, IEntityCloneable<CB_Email>
    {
        
        public class CB_EmailComparer : IEqualityComparer<CB_Email>
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
                return new CB_EmailComparer();
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

        public CB_Email Clone(object obj)
        {
            if (obj == null)
            {
                return default(CB_Email);
            }
            CB_Email actObj = obj as CB_Email;
            CB_Email retObj = new CB_Email() {
            ContactId = actObj.ContactId,
            Email = actObj.Email,
            EmailId = actObj.EmailId,
            EmailTypeId = actObj.EmailTypeId
            };
            return retObj;
        }
    }
}