using System.ComponentModel.DataAnnotations;
namespace ContactBook.Domain.Models
{
    public class SpecialDate
    {
        public int SpecialDateId { get; set; }

        public long ContactId { get; set; }
        [Required]
        public System.DateTime Dates { get; set; }

        public int SpecialDateTpId { get; set; }
    }
}