namespace ContactBook.Domain.Models
{
    public class ContactBookInfo
    {
        public long BookId { get; set; }

        public string BookName { get; set; }

        public bool Enabled { get; set; }

        public string Username { get; set; }
    }
}