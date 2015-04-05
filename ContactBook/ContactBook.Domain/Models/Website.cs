namespace ContactBook.Domain.Models
{
    public class Website
    {
        public int WebsiteId { get; set; }

        public string WebsiteUrl { get; set; }

        public long ContactId { get; set; }
    }
}