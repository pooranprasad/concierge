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
    
    public partial class PassportType
    {
        public PassportType()
        {
            this.Passports = new HashSet<Passport>();
        }
    
        public System.Guid PassportTypeId { get; set; }
        public string PassportTypeName { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Passport> Passports { get; set; }
    }
}
