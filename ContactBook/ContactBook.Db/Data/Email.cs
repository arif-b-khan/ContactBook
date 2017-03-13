namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Email
    {
        public int EmailId { get; set; }

        public long ContactId { get; set; }

        [Column("Email")]
        [Required]
        public string Email1 { get; set; }

        public int? EmailTypeId { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual EmailType EmailType { get; set; }
    }
}
