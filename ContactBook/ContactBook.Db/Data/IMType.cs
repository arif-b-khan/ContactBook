namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class IMType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IMType()
        {
            IMs = new HashSet<IM>();
        }

        public int IMTypeId { get; set; }

        [Required]
        [StringLength(30)]
        public string IM_TypeName { get; set; }

        [StringLength(2000)]
        public string IMLogoPath { get; set; }

        public long? BookId { get; set; }

        public virtual ContactBook ContactBook { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IM> IMs { get; set; }
    }
}
