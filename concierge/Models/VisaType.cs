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
    
    public partial class VisaType
    {
        public VisaType()
        {
            this.CountryVisas = new HashSet<CountryVisa>();
        }
    
        public byte VisaTypeId { get; set; }
        public string VisaTypeName { get; set; }
    
        public virtual ICollection<CountryVisa> CountryVisas { get; set; }
    }
}
