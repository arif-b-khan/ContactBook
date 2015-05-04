using System;
namespace ContactBook.Domain.Contexts
{
    interface IContactBookSecretContext
    {
        System.Collections.Generic.Dictionary<string, string> GetKeyValue();
    }
}
