//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ContactBook.Db.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class CB_GroupType
    {
        public CB_GroupType()
        {
            this.CB_Book_GroupTypes = new HashSet<CB_ContactByGroup>();
        }
    
        public int GroupId { get; set; }
        public string Group_TypeName { get; set; }
        public Nullable<long> BookId { get; set; }
    
        public virtual ICollection<CB_ContactByGroup> CB_Book_GroupTypes { get; set; }
        public virtual CB_ContactBook CB_ContactBook { get; set; }
    }
}