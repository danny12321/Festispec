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
    
    public partial class Contactpersons
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contactpersons()
        {
            this.Festivals = new HashSet<Festivals>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public Nullable<int> client_id { get; set; }
        public Nullable<int> type_contact { get; set; }
    
        public virtual Clients Clients { get; set; }
        public virtual Type_contacts Type_contacts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Festivals> Festivals { get; set; }
    }
}
