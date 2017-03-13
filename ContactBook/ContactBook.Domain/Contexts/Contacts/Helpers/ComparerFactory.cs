using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;

namespace ContactBook.Domain.Contexts.Contacts.Helpers
{
    public class ComparerFactory
    {
        public static IEqualityComparer<T> GetComparer<T>()
        {
            IEqualityComparer<T> retComparer = null;
            switch (typeof(T).Name)
            {
                case "CB_Number":
                    retComparer = Number.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_Email":
                    retComparer = Email.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_IM":
                    retComparer = IM.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_Address":
                    retComparer = Db.Data.Address.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_Relationship":
                    retComparer = Relationship.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_InternetCall":
                    retComparer = InternetCall.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_Website":
                    retComparer = Website.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_SpecialDate":
                    retComparer = SpecialDate.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_ContactByGroup":
                    retComparer = ContactByGroup.Comparer as IEqualityComparer<T>;
                    break;
                default:
                    retComparer = null;
                    break;

            }
            return retComparer;
        }
    }
}
