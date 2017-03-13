using ContactBook.Db.Data;
using ContactBook.Domain.Contexts.Generics;
using ContactBook.Domain.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ContactBook.Domain.Validations.Helper
{
    public class TypeExistsHelper
    {
        public static ValidationResult GetTypeName(string typeName, string typeValue, long bookId)
        {
            switch (typeName)
            {
                case "NumberType":
                    IGenericContextTypes<NumberTypeModel, NumberType> numberType = new GenericContextTypes<NumberTypeModel, NumberType>();

                    foreach (string item in numberType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.NumberTypeName))
                    {
                        if (item.Equals(typeValue, StringComparison.OrdinalIgnoreCase))
                        {
                            return new ValidationResult("Number type name with this value already exists");
                        }
                    }
                    break;

                case "AddressType":
                    IGenericContextTypes<AddressTypeModel, AddressType> addressType = new GenericContextTypes<AddressTypeModel, AddressType>();

                    foreach (string item in addressType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.AddressTypeName))
                    {
                        if (item.Trim().Equals(typeValue.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return new ValidationResult("Address type name with this value already exists");
                        }
                    }
                    break;

                case "EmailType":
                    IGenericContextTypes<EmailTypeModel, EmailType> emailType = new GenericContextTypes<EmailTypeModel, EmailType>();

                    foreach (string item in emailType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.EmailTypeName))
                    {
                        if (item.Trim().Equals(typeValue.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return new ValidationResult("Email type name with this value already exists");
                        }
                    }
                    break;

                case "IMType":
                    IGenericContextTypes<IMTypeModel, IMType> imType = new GenericContextTypes<IMTypeModel, IMType>();

                    foreach (string item in imType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.IMTypeName))
                    {
                        if (item.Trim().Equals(typeValue.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return new ValidationResult("Address type name with this value already exists");
                        }
                    }
                    break;

                case "GroupType":
                    var groupType = new GenericContextTypes<GroupTypeModel, GroupType>();

                    foreach (string item in groupType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.GroupTypeName))
                    {
                        if (item.Trim().Equals(typeValue.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return new ValidationResult("Group type name with this value already exists");
                        }
                    }
                    break;

                case "RelationshipType":
                    var relationshipType = new GenericContextTypes<RelationshipTypeModel, RelationshipType>();

                    foreach (string item in relationshipType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.RelationshipTypeName))
                    {
                        if (item.Trim().Equals(typeValue.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return new ValidationResult("Relationship type name with this value already exists");
                        }
                    }
                    break;
                case "SpecialDateType":
                    var specialDateType = new GenericContextTypes<SpecialDateTypeModel, SpecialDateType>();

                    foreach (string item in specialDateType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.DateTypeName))
                    {
                        if (item.Trim().Equals(typeValue.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return new ValidationResult("Special date type name with this value already exists");
                        }
                    }
                    break;
                default:
                    throw new Exception("Unable to find rule for this type");
            }

            return null;
        }
    }
}