namespace ContactBook.Domain.Models
{
    public class SpecialDates
    {
        public int SpecialDateId { get; set; }

        public long ContactId { get; set; }

        public System.DateTime Dates { get; set; }

        public int SpecialDateTpId { get; set; }
    }
}