namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class GroupType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GroupType()
        {
            ContactByGroups = new HashSet<ContactByGroup>();
        }

        [Key]
        public int GroupId { get; set; }

        [Required]
        [StringLength(100)]
        public string Group_TypeName { get; set; }

        public long? BookId { get; set; }

        public virtual ContactBook ContactBook { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContactByGroup> ContactByGroups { get; set; }
    }
}
