using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ContactBook.Domain.Contexts;
using ContactBook.Domain.Contexts.Contacts;
using ContactBook.Domain.Models;
using ContactBook.Domain.IoC;

namespace ContactBook.Domain.Validations
{
    public class ValidateContactIdAttribute : ValidationAttribute
    {
        const string ErrorMessageFormat = "Contact {0} doesn't exists";
        private IPrincipal userPrincipal;

        public IPrincipal UserPrincipal
        {
            get
            {
                if (userPrincipal == null)
                {
                    if (HttpContext.Current.User != null)
                    {
                        userPrincipal = HttpContext.Current.User;
                    }
                }
                return userPrincipal;
            }
            set { userPrincipal = value; }
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Int32 contactId;

            if (value == null)
            {
                return new ValidationResult("Invalid Contact Id.");
            }

            if (Int32.TryParse(Convert.ToString(value), out contactId))
            {
                IContactContext contactContext = DependencyFactory.Resolve<IContactContext>();
                IContactBookContext bookContext = DependencyFactory.Resolve<IContactBookContext>();

                ContactBookInfo cbInfo = bookContext.GetContactBook(UserPrincipal.Identity.Name);

                if (cbInfo != null)
                {
                    Contact contact = contactContext.GetContact(cbInfo.BookId, contactId);
                    if (contact != null)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult(string.Format(ErrorMessageFormat, contactId));
                    }
                }
                else
                {
                    return new ValidationResult(string.Format(ErrorMessageFormat, contactId));
                }
            }
            else
            {
                return new ValidationResult("Invalid Contact. Please provide valid contact id");
            }
        }
    }
}
