using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Microsoft.Owin.Security.DataProtection;

namespace ContactBook.WebApi.Common
{
    public class ContactBookMachineKeyDataProvider : IDataProtectionProvider
    {
        public IDataProtector Create(params string[] purposes)
        {
            return new ContactBookDataProtector(purposes);
        }
    }

    public class ContactBookDataProtector : IDataProtector
    {
        public readonly string[] purposes;
        public ContactBookDataProtector(string [] pPurposes)
        {
            purposes = pPurposes;
        }

        public byte[] Protect(byte[] userData)
        {
            return MachineKey.Protect(userData, purposes);
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            return MachineKey.Unprotect(protectedData, purposes);
        }
    }

}