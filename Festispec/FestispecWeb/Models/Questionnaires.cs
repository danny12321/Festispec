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
    
    public partial class Questionnaires
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Questionnaires()
        {
            this.Questions = new HashSet<Questions>();
        }
    
        public int id { get; set; }
        public string version { get; set; }
        public Nullable<int> inspector_id { get; set; }
        public Nullable<int> inspection_id { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> finished { get; set; }
    
        public virtual Inspectors_at_inspection Inspectors_at_inspection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Questions> Questions { get; set; }
    }
}