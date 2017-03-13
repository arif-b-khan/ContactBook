namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class InternetCall
    {
        public int InternetCallId { get; set; }

        [Required]
        [StringLength(100)]
        public string InternetCallNumber { get; set; }

        public long ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
