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
    
    public partial class Answers
    {
        public int id { get; set; }
        public string answer { get; set; }
        public Nullable<int> question_id { get; set; }
    
        public virtual Questions Questions { get; set; }
    }
}
