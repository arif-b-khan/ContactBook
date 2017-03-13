using System.ComponentModel.DataAnnotations;
namespace ContactBook.Domain.Models
{
    public class WebsiteModel
    {
        public int WebsiteId { get; set; }

        [Required]
        [StringLength(1000)]
        public string WebsiteUrl { get; set; }

        public long ContactId { get; set; }
    }
}