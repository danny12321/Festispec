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
    
    public partial class Inspectors_at_inspection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inspectors_at_inspection()
        {
            this.Questionnaires = new HashSet<Questionnaires>();
        }
    
        public int inpector_id { get; set; }
        public int inspection_id { get; set; }
        public System.DateTime absent { get; set; }
    
        public virtual Inspections Inspections { get; set; }
        public virtual Inspectors Inspectors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Questionnaires> Questionnaires { get; set; }
    }
}
