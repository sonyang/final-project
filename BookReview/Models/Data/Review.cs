//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookReview.Models.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Review
    {
        public System.Guid ID { get; set; }
        public System.Guid BookID { get; set; }
        public System.Guid UserID { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public decimal Rating { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
