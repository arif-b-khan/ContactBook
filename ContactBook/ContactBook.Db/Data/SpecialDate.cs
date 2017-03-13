namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SpecialDate
    {
        public int SpecialDateId { get; set; }

        public long ContactId { get; set; }

        public DateTime Dates { get; set; }

        public int SpecialDateTpId { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual SpecialDateType SpecialDateType { get; set; }
    }
}
