using ContactBook.Domain.Validations.Helper;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ContactBook.Domain.Validations
{
    public class TypeExistsAttribute : ValidationAttribute
    {
        private string bookIdProperty;

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

            ValidationResult result = TypeExistsHelper.GetTypeName(validationContext.ObjectInstance.GetType().Name, typeValue, bookId);

            if (result != null)
            {
                return result;
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}