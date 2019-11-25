//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Festispec.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Inspectors
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inspectors()
        {
            this.Inspectors_at_inspection = new HashSet<Inspectors_at_inspection>();
            this.Inspectors_availability = new HashSet<Inspectors_availability>();
            this.Users = new HashSet<Users>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public Nullable<System.DateTime> birthday { get; set; }
        public string postalcode { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string housenumber { get; set; }
        public string phone { get; set; }
        public Nullable<System.DateTime> active { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inspectors_at_inspection> Inspectors_at_inspection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inspectors_availability> Inspectors_availability { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
