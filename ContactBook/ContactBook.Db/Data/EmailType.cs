namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EmailType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmailType()
        {
            Emails = new HashSet<Email>();
        }

        public int EmailTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string Email_TypeName { get; set; }

        public long? BookId { get; set; }

        public virtual ContactBook ContactBook { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Email> Emails { get; set; }
    }
}
