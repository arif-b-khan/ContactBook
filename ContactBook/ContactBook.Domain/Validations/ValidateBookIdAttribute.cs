using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ContactBook.Db.Repositories;
using ContactBook.Domain.Contexts;
using ContactBook.Domain.IoC;
using ContactBook.Domain.Models;

namespace ContactBook.Domain.Validations
{
    public class ValidateBookIdAttribute : ValidationAttribute
    {
        private IPrincipal userPrincipal;

        public IPrincipal UserPrincipal
        {
            get {
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
                IContactBookContext cbContext = new ContactBookContext();

                ContactBookInfo cbInfo = cbContext.GetContactBook(UserPrincipal.Identity.Name);

                if (cbInfo != null)
                {
                    if (cbInfo.BookId == bookId)
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult(string.Format("BookId {0} is not match with bookid saved in database for this user", bookId));
                    }
                }
                else
                {
                    return new ValidationResult(string.Format("BookId {0} is not match with bookid saved in database for this user"));
                }
            }
            else
            {
                return new ValidationResult("Invalid bookId. Please provide valid book id");
            }
        }
    }
}
