﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContactBook.Db
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ContactBookEdmContainer : DbContext
    {
        public ContactBookEdmContainer()
            : base("name=ContactBookEdmContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<CB_ContactBook> CB_ContactBook { get; set; }
        public DbSet<CB_Number> CB_Number { get; set; }
        public DbSet<CB_Email> CB_Email { get; set; }
        public DbSet<CB_Address> CB_Address { get; set; }
        public DbSet<CB_Group> CB_Group { get; set; }
        public DbSet<CB_NumberType> CB_NumberType { get; set; }
        public DbSet<CB_Book_GroupTypes> CB_Book_GroupTypes { get; set; }
        public DbSet<CB_AddressType> CB_AddressType { get; set; }
        public DbSet<CB_EmailType> CB_EmailType { get; set; }
        public DbSet<CB_Suffix> CB_Suffix { get; set; }
        public DbSet<CB_IM> CB_IM { get; set; }
        public DbSet<CB_IMType> CB_IMType { get; set; }
        public DbSet<CB_Website> CB_Website { get; set; }
        public DbSet<CB_Relationship> CB_Relationship { get; set; }
        public DbSet<CB_RelationshipType> CB_RelationshipType { get; set; }
        public DbSet<CB_SpecialDates> CB_SpecialDates { get; set; }
        public DbSet<CB_SpecialDateType> CB_SpecialDateType { get; set; }
        public DbSet<CB_InternetCall> CB_InternetCall { get; set; }
    }
}
