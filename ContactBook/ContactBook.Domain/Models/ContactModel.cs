using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ContactBook.Domain.Validations;

namespace ContactBook.Domain.Models
{
    public class ContactModel
    {
        public long ContactId { get; set; }

        [Required]
        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Middlename { get; set; }

        public string Suffix { get; set; }

        public string PhFirstname { get; set; }

        public string PhMiddlename { get; set; }

        public string PhSurname { get; set; }

        public string CompanyName { get; set; }

        public string JobTitle { get; set; }

        public string Notes { get; set; }

        public string NickName { get; set; }
        
        [ValidateBookId]
        public long BookId { get; set; }

        public ICollection<AddressModel> Addresses { get; set; }

        public ICollection<NumberModel> Numbers { get; set; }

        public ICollection<EmailModel> Emails { get; set; }

        public ICollection<ContactByGroupModel> ContactByGroups { get; set; }

        public ICollection<IMModel> IMs { get; set; }

        public ICollection<WebsiteModel> Websites { get; set; }

        public ICollection<RelationshipModel> Relationships { get; set; }

        public ICollection<SpecialDateModel> SpecialDates { get; set; }

        public ICollection<InternetCallModel> InternetCalls { get; set; }
    }
}