namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Secret
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string SecretKey { get; set; }

        [Required]
        [StringLength(200)]
        public string SecretValue { get; set; }
    }
}
