namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class RelationshipType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RelationshipType()
        {
            Relationships = new HashSet<Relationship>();
        }

        public int RelationshipTypeId { get; set; }

        [Required]
        public string Relationship_TypeName { get; set; }

        public long? BookId { get; set; }

        public virtual ContactBook ContactBook { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Relationship> Relationships { get; set; }
    }
}
