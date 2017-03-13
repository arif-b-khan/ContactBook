using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class InternetCall : IEquatable<InternetCall>, INewEntity<InternetCall>, IEntityCloneable<InternetCall>
    {
        public static IEqualityComparer<InternetCall> Comparer
        {
            get
            {
                return new CBInternetCallComparer();
            }
        }

        public class CBInternetCallComparer : IEqualityComparer<InternetCall>
        {
            public CBInternetCallComparer()
            {

            }
            public bool Equals(InternetCall x, InternetCall y)
            {
                if (x.InternetCallId == y.InternetCallId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(InternetCall obj)
            {
                return obj.InternetCallId.GetHashCode();
            }
        }

        public bool Equals(InternetCall other)
        {
            if (other.InternetCallId.Equals(this.InternetCallId)
                && other.InternetCallNumber == this.InternetCallNumber
                && other.ContactId.Equals(this.ContactId))
            {
                return true;
            }
            return false;
        }

        public bool Equals(int id)
        {
            if (this.InternetCallId.Equals(id))
            {
                return true;
            }
            return false;
        }

        public InternetCall Clone()
        {
            InternetCall cloneInternetCall = new InternetCall();
            cloneInternetCall.ContactId = this.ContactId;
            cloneInternetCall.InternetCallNumber = this.InternetCallNumber;
            cloneInternetCall.InternetCallId = this.InternetCallId;
            return cloneInternetCall;
        }

    }
}
