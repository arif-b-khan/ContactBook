using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactBook.Domain.Models
{
    public class MdlContact
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

        public  ICollection<MdlAddress> Addresses { get; set; }
        public ICollection<MdlNumber> Numbers { get; set; }

        public ICollection<MdlEmail> Emails { get; set; }
        public ICollection<MdlContactByGroup> ContactByGroups { get; set; }
        public ICollection<MdlIM> IMs { get; set; }
        public ICollection<MdlWebsite> Websites { get; set; }
        public ICollection<MdlRelationship> Relationships { get; set; }
        public ICollection<MdlSpecialDates> SpecialDates { get; set; }
        public ICollection<MdlInternetCall> InternetCalls { get; set; }

    }
}
