namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contact
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contact()
        {
            Addresses = new HashSet<Address>();
            ContactByGroups = new HashSet<ContactByGroup>();
            Emails = new HashSet<Email>();
            InternetCalls = new HashSet<InternetCall>();
            Numbers = new HashSet<Number>();
            Relationships = new HashSet<Relationship>();
            SpecialDates = new HashSet<SpecialDate>();
            Websites = new HashSet<Website>();
            IMs = new HashSet<IM>();
        }

        public long ContactId { get; set; }

        [Required]
        [StringLength(100)]
        public string Firstname { get; set; }

        [StringLength(100)]
        public string Lastname { get; set; }

        public string Middlename { get; set; }

        [StringLength(10)]
        public string Suffix { get; set; }

        [StringLength(100)]
        public string PhFirstname { get; set; }

        public string PhMiddlename { get; set; }

        public string PhSurname { get; set; }

        public string CompanyName { get; set; }

        public string JobTitle { get; set; }

        public string Notes { get; set; }

        [StringLength(50)]
        public string NickName { get; set; }

        public long BookId { get; set; }

        public string ImagePath { get; set; }

        public string ThumbnailPath { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Address> Addresses { get; set; }

        public virtual ContactBook ContactBook { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ContactByGroup> ContactByGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Email> Emails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InternetCall> InternetCalls { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Number> Numbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Relationship> Relationships { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpecialDate> SpecialDates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Website> Websites { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IM> IMs { get; set; }
    }
}
