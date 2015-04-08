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
                    retComparer = CB_Number.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_Email":
                    retComparer = CB_Email.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_IM":
                    retComparer = CB_IM.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_Address":
                    retComparer = CB_Address.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_Relationship":
                    retComparer = CB_Relationship.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_InternetCall":
                    retComparer = CB_InternetCall.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_Website":
                    retComparer = CB_Website.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_SpecialDate":
                    retComparer = CB_SpecialDate.Comparer as IEqualityComparer<T>;
                    break;
                case "CB_ContactByGroup":
                    retComparer = CB_ContactByGroup.Comparer as IEqualityComparer<T>;
                    break;
                default:
                    retComparer = null;
                    break;

            }
            return retComparer;
        }
    }
}
