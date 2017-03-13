namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IM
    {
        public int IMId { get; set; }

        [Required]
        public string Username { get; set; }

        public long ContactId { get; set; }

        public int IMTypeId { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual IMType IMType { get; set; }
    }
}
