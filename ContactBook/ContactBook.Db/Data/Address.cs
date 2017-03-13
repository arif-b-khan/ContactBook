namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Address
    {
        public int AddressId { get; set; }

        public long ContactId { get; set; }

        [Column("Address")]
        [Required]
        public string Address1 { get; set; }

        public int? AddressTypeId { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual AddressType AddressType { get; set; }
    }
}
