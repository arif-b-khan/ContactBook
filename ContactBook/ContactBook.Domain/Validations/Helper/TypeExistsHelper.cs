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
                    IGenericContextTypes<NumberType, CB_NumberType> numberType = new GenericContextTypes<NumberType, CB_NumberType>();

                    foreach (string item in numberType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.NumberTypeName))
                    {
                        if (item.Equals(typeValue, StringComparison.OrdinalIgnoreCase))
                        {
                            return new ValidationResult("Number type name with this value already exists");
                        }
                    }
                    break;

                case "AddressType":
                    IGenericContextTypes<AddressType, CB_AddressType> addressType = new GenericContextTypes<AddressType, CB_AddressType>();

                    foreach (string item in addressType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.AddressTypeName))
                    {
                        if (item.Trim().Equals(typeValue.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return new ValidationResult("Address type name with this value already exists");
                        }
                    }
                    break;

                case "EmailType":
                    IGenericContextTypes<EmailType, CB_EmailType> emailType = new GenericContextTypes<EmailType, CB_EmailType>();

                    foreach (string item in emailType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.EmailTypeName))
                    {
                        if (item.Trim().Equals(typeValue.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return new ValidationResult("Email type name with this value already exists");
                        }
                    }
                    break;

                case "IMType":
                    IGenericContextTypes<IMType, CB_IMType> imType = new GenericContextTypes<IMType, CB_IMType>();

                    foreach (string item in imType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.IMTypeName))
                    {
                        if (item.Trim().Equals(typeValue.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return new ValidationResult("Address type name with this value already exists");
                        }
                    }
                    break;

                case "GroupType":
                    IGenericContextTypes<GroupType, CB_GroupType> groupType = new GenericContextTypes<GroupType, CB_GroupType>();

                    foreach (string item in groupType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.GroupTypeName))
                    {
                        if (item.Trim().Equals(typeValue.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return new ValidationResult("Group type name with this value already exists");
                        }
                    }
                    break;

                case "RelationshipType":
                    IGenericContextTypes<RelationshipType, CB_RelationshipType> relationshipType = new GenericContextTypes<RelationshipType, CB_RelationshipType>();

                    foreach (string item in relationshipType.GetTypes(cbt => ((cbt.BookId.HasValue && cbt.BookId.Value == bookId) || !cbt.BookId.HasValue)).Select(ad => ad.RelationshipTypeName))
                    {
                        if (item.Trim().Equals(typeValue.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return new ValidationResult("Relationship type name with this value already exists");
                        }
                    }
                    break;
                case "SpecialDateType":
                    IGenericContextTypes<SpecialDateType, CB_SpecialDateType> specialDateType = new GenericContextTypes<SpecialDateType, CB_SpecialDateType>();

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