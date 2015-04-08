using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Db.Data
{
    public partial class CB_InternetCall : IEquatable<CB_InternetCall>, INewEntity<CB_InternetCall>, IEntityCloneable<CB_InternetCall>
    {
        public static IEqualityComparer<CB_InternetCall> Comparer
        {
            get
            {
                return new CBInternetCallComparer();
            }
        }

        public class CBInternetCallComparer : IEqualityComparer<CB_InternetCall>
        {
            public CBInternetCallComparer()
            {

            }
            public bool Equals(CB_InternetCall x, CB_InternetCall y)
            {
                if (x.InternetCallId == y.InternetCallId)
                {
                    return true;
                }
                return false;
            }

            public int GetHashCode(CB_InternetCall obj)
            {
                return obj.InternetCallId.GetHashCode();
            }
        }

        public bool Equals(CB_InternetCall other)
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

        public CB_InternetCall Clone()
        {
            CB_InternetCall cloneInternetCall = new CB_InternetCall();
            cloneInternetCall.ContactId = this.ContactId;
            cloneInternetCall.InternetCallNumber = this.InternetCallNumber;
            cloneInternetCall.InternetCallId = this.InternetCallId;
            return cloneInternetCall;
        }

    }
}
