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
    
    public partial class City
    {
        public int CityId { get; set; }
        public int StateId { get; set; }
        public string CityName { get; set; }
        public Nullable<bool> IsStateCapital { get; set; }
        public Nullable<bool> IsCountryCapital { get; set; }
    
        public virtual State State { get; set; }
    }
}
