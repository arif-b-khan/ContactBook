using System;

namespace ContactBook.Domain.Models
{
    public class Email
    {
        public int EmailId { get; set; }

        public long ContactId { get; set; }

        public string EmailAddress { get; set; }

        public Nullable<int> EmailTypeId { get; set; }

    }
}