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
    
    public partial class Festivals
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Festivals()
        {
            this.Contactpersons = new HashSet<Contactpersons>();
            this.Inspections = new HashSet<Inspections>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string postalcode { get; set; }
        public string street { get; set; }
        public string housenumber { get; set; }
        public string country { get; set; }
        public System.DateTime start_date { get; set; }
        public System.DateTime end_date { get; set; }
        public int client_id { get; set; }
        public Nullable<int> municipality_id { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string city { get; set; }
    
        public virtual Clients Clients { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contactpersons> Contactpersons { get; set; }
        public virtual Municipalities Municipalities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inspections> Inspections { get; set; }
    }
}
