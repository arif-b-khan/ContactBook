namespace ContactBook.Domain.Models
{
    public class IM
    {
        public int IMId { get; set; }

        public string Username { get; set; }

        public long ContactId { get; set; }

        public int IMTypeId { get; set; }
    }
}