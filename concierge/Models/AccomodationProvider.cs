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
    
    public partial class AccomodationProvider
    {
        public AccomodationProvider()
        {
            this.Accomodations = new HashSet<Accomodation>();
        }
    
        public System.Guid AccomodationProviderId { get; set; }
        public string ProviderName { get; set; }
        public string ProviderDetails { get; set; }
    
        public virtual ICollection<Accomodation> Accomodations { get; set; }
    }
}