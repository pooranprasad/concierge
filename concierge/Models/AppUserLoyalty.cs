//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace concierge.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AppUserLoyalty
    {
        public System.Guid AppUserLoyaltyId { get; set; }
        public System.Guid AppUserId { get; set; }
        public System.Guid LoyaltyId { get; set; }
        public string LoyaltyNumber { get; set; }
        public Nullable<double> Points { get; set; }
    
        public virtual AppUser AppUser { get; set; }
        public virtual Loyalty Loyalty { get; set; }
    }
}