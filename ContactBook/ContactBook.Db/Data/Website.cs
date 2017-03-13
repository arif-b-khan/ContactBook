namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Website
    {
        public int WebsiteId { get; set; }

        [Column("Website")]
        [StringLength(1000)]
        public string Website1 { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public long ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}
