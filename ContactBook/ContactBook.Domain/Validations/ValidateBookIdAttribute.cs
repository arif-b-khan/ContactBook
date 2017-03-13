using ContactBook.Domain.Contexts;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Web;

namespace ContactBook.Domain.Validations
{
    public class ValidateBookIdAttribute : ValidationAttribute
    {
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

        public ValidateBookIdAttribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            long bookId;

            if (value == null)
            {
                return new ValidationResult("Invalid bookId.");
            }

            if (Int64.TryParse(Convert.ToString(value), out bookId))
            {
                IContactBookContext cbContext = DependencyFactory.Resolve<IContactBookContext>();

                ContactBookInfoModel cbInfo = cbContext.GetContactBook(UserPrincipal.Identity.Name);

                if (cbInfo != null)
                {
                    if (cbInfo.BookId == bookId)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult(string.Format("BookId {0} is not matching with bookid saved in database for this user", bookId));
                    }
                }
                else
                {
                    return new ValidationResult(string.Format("BookId {0} is not matching with bookid saved in database for this user", bookId));
                }
            }
            else
            {
                return new ValidationResult("Invalid bookId. Please provide valid book id");
            }
        }
    }
}