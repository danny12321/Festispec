//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FestispecWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Clients
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Clients()
        {
            this.Contactpersons = new HashSet<Contactpersons>();
            this.Festivals = new HashSet<Festivals>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string postalcode { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string housenumber { get; set; }
        public string phone { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contactpersons> Contactpersons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Festivals> Festivals { get; set; }
    }
}
