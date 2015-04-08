using System.ComponentModel.DataAnnotations;
namespace ContactBook.Domain.Models
{
    public class InternetCall
    {
        public int InternetCallId { get; set; }
        
        [Required]
        [StringLength(20)]
        public string InternetCallNumber { get; set; }

        public long ContactId { get; set; }

    }
}