﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Festipec.Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FestispecEntities : DbContext
    {
        public FestispecEntities()
            : base("name=FestispecEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Contactpersons> Contactpersons { get; set; }
        public virtual DbSet<Festivals> Festivals { get; set; }
        public virtual DbSet<Inspections> Inspections { get; set; }
        public virtual DbSet<Inspectors> Inspectors { get; set; }
        public virtual DbSet<Inspectors_at_inspection> Inspectors_at_inspection { get; set; }
        public virtual DbSet<Inspectors_availability> Inspectors_availability { get; set; }
        public virtual DbSet<Laws> Laws { get; set; }
        public virtual DbSet<Municipalities> Municipalities { get; set; }
        public virtual DbSet<Possible_answer> Possible_answer { get; set; }
        public virtual DbSet<Questionnaires> Questionnaires { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Quotations> Quotations { get; set; }
        public virtual DbSet<Rolls> Rolls { get; set; }
        public virtual DbSet<Type_contacts> Type_contacts { get; set; }
        public virtual DbSet<Type_questions> Type_questions { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
