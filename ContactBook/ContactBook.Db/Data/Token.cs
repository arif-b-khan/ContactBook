namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Token
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string UserId { get; set; }

        [Column("Token")]
        [StringLength(500)]
        public string Token1 { get; set; }

        public string TokenType { get; set; }

        public Guid Identifier { get; set; }
    }
}
