namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ContactByGroup
    {
        [Key]
        public int GroupRelationId { get; set; }

        public long ContactId { get; set; }

        public int GroupId { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual GroupType GroupType { get; set; }
    }
}
