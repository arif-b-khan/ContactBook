namespace ContactBook.Db.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ContactbookDbContext : DbContext
    {
        public ContactbookDbContext()
            : base("name=ContactbookDbContext")
        {
        }

        public ContactbookDbContext(string connectionName):base(connectionName)
        {

        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<ContactBook> ContactBooks { get; set; }
        public virtual DbSet<ContactByGroup> ContactByGroups { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<EmailType> EmailTypes { get; set; }
        public virtual DbSet<GroupType> GroupTypes { get; set; }
        public virtual DbSet<IM> IMs { get; set; }
        public virtual DbSet<IMType> IMTypes { get; set; }
        public virtual DbSet<InternetCall> InternetCalls { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Number> Numbers { get; set; }
        public virtual DbSet<NumberType> NumberTypes { get; set; }
        public virtual DbSet<Relationship> Relationships { get; set; }
        public virtual DbSet<RelationshipType> RelationshipTypes { get; set; }
        public virtual DbSet<Secret> Secrets { get; set; }
        public virtual DbSet<SpecialDate> SpecialDates { get; set; }
        public virtual DbSet<SpecialDateType> SpecialDateTypes { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<Website> Websites { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactBook>()
                .HasMany(e => e.Contacts)
                .WithRequired(e => e.ContactBook)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<IMType>()
                .HasMany(e => e.IMs)
                .WithRequired(e => e.IMType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Application)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Level)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Logger)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.MachineName)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.CallSite)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Thread)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Exception)
                .IsUnicode(false);

            modelBuilder.Entity<Log>()
                .Property(e => e.Stacktrace)
                .IsUnicode(false);

            modelBuilder.Entity<SpecialDateType>()
                .HasMany(e => e.SpecialDates)
                .WithRequired(e => e.SpecialDateType)
                .WillCascadeOnDelete(false);
        }
    }
}
