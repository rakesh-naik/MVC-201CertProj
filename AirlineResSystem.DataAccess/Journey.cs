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
    
    public partial class Journey
    {
        public int journey_id { get; set; }
        public int source { get; set; }
        public int dest { get; set; }
        public decimal cost { get; set; }
        public string route { get; set; }
    
        public virtual City City { get; set; }
    }
}