namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Relationship
    {
        public int RelationshipId { get; set; }

        public long ContactId { get; set; }

        public int? RelationshipTypeId { get; set; }

        public virtual Contact Contact { get; set; }

        public virtual RelationshipType RelationshipType { get; set; }
    }
}
