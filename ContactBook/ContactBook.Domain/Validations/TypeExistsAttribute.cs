using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ContactBook.Db.Data;
using ContactBook.Domain.Contexts.Generics;
using ContactBook.Domain.Models;

namespace ContactBook.Domain.Validations
{
    public class TypeExistsAttribute : ValidationAttribute
    {
        string bookIdProperty;

        public TypeExistsAttribute(string bookId)
        {
            if (string.IsNullOrEmpty(bookId))
            {
                this.bookIdProperty = "BookId";
            }
            else
            {
                this.bookIdProperty = bookId;
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string typeValue = Convert.ToString(value);
            if (typeValue == null && string.IsNullOrEmpty(typeValue))
            {
                return new ValidationResult("Type cannot be empty");
            }

            PropertyInfo bookIdProp = validationContext.ObjectInstance.GetType().GetProperty(bookIdProperty);
            object bookIdObj = bookIdProp.GetValue(validationContext.ObjectInstance, null);

            if (bookIdObj == null)
            {
                return new ValidationResult("Matching bookid not found");
            }

            long bookId;

            if (!Int64.TryParse(Convert.ToString(bookIdObj), out bookId))
            {
                return new ValidationResult("Invalid bookId");
            }

            IGenericContextTypes<AddressType, CB_AddressType> addressTypes = new GenericContextTypes<AddressType, CB_AddressType>();

            foreach (string item in addressTypes.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.AddressTypeName))
            {
                if (item.Equals(typeValue, StringComparison.OrdinalIgnoreCase))
                {
                    return new ValidationResult("Type name with this value already exists");
                }
            }

            return ValidationResult.Success;
        }
    }
}
