using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactBook.Domain.Models
{
    public class Contact
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

        public long BookId { get; set; }

        public ICollection<Address> Addresses { get; set; }

        public ICollection<Number> Numbers { get; set; }

        public ICollection<Email> Emails { get; set; }

        public ICollection<ContactByGroup> ContactByGroups { get; set; }

        public ICollection<IM> IMs { get; set; }

        public ICollection<Website> Websites { get; set; }

        public ICollection<Relationship> Relationships { get; set; }

        public ICollection<SpecialDates> SpecialDates { get; set; }

        public ICollection<InternetCall> InternetCalls { get; set; }
    }
}