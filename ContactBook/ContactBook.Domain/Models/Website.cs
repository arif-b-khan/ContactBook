using System.ComponentModel.DataAnnotations;
namespace ContactBook.Domain.Models
{
    public class Website
    {
        public int WebsiteId { get; set; }

        [Required]
        [StringLength(1000)]
        public string WebsiteUrl { get; set; }

        public long ContactId { get; set; }
    }
}