//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AirlineResSystem.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class FareMapping
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FareMapping()
        {
            this.Ticketing_Info = new HashSet<Ticketing_Info>();
        }
    
        public int Fare_id { get; set; }
        public int journey_id { get; set; }
        public string @class { get; set; }
        public decimal cost { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Journey Journey { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ticketing_Info> Ticketing_Info { get; set; }
    }
}
