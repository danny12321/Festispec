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
    
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.Festivals = new HashSet<Festival>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string postalcode { get; set; }
        public string street { get; set; }
        public string housenumber { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
    
        public virtual Contactperson Contactperson { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Festival> Festivals { get; set; }
        public virtual Quotation Quotation { get; set; }
    }
}
