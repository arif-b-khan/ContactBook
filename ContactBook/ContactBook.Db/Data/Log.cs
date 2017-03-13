namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log
    {
        [Key]
        public Guid LoggingId { get; set; }

        public DateTime? Date { get; set; }

        [StringLength(200)]
        public string Application { get; set; }

        [StringLength(100)]
        public string Level { get; set; }

        [StringLength(8000)]
        public string Logger { get; set; }

        [StringLength(8000)]
        public string Message { get; set; }

        [StringLength(8000)]
        public string MachineName { get; set; }

        [StringLength(8000)]
        public string UserName { get; set; }

        [StringLength(8000)]
        public string CallSite { get; set; }

        [StringLength(100)]
        public string Thread { get; set; }

        [StringLength(8000)]
        public string Exception { get; set; }

        [StringLength(8000)]
        public string Stacktrace { get; set; }
    }
}
