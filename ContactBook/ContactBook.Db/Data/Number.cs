namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Number
    {
        public int NumberId { get; set; }

        public long ContactId { get; set; }

        [Column("Number")]
        [Required]
        [StringLength(30)]
        public string Number1 { get; set; }

        public int? NumberTypeId { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual NumberType NumberType { get; set; }
    }
}
