namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NumberType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NumberType()
        {
            Numbers = new HashSet<Number>();
        }

        public int NumberTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Number_TypeName { get; set; }

        public long? BookId { get; set; }

        public virtual ContactBook ContactBook { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Number> Numbers { get; set; }
    }
}
