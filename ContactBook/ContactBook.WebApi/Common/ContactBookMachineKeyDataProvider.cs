using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using ContactBook.Domain.Common.Logging;
using ContactBook.Domain.IoC;
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
        public static readonly ICBLogger _logger = DependencyFactory.Resolve<ICBLogger>();

        public readonly string[] purposes;
        public ContactBookDataProtector(string[] pPurposes)
        {
            purposes = pPurposes;
        }

        public byte[] Protect(byte[] userData)
        {
            byte[] retByte = MachineKey.Protect(userData, purposes);
            string strData = Convert.ToBase64String(retByte);
            _logger.Info("Protected Key: " + strData);
            return retByte;
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            byte[] retByte = MachineKey.Unprotect(protectedData, purposes);
            string strData = Convert.ToBase64String(retByte);
            _logger.Info("Unprotected Key: " + strData);
            return retByte;
        }
    }

}