using System.ComponentModel.DataAnnotations;
namespace ContactBook.Domain.Models
{
    public class IM
    {
        public int IMId { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        public long ContactId { get; set; }

        public int IMTypeId { get; set; }

    }
}