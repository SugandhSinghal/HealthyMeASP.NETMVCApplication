﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HealthyMe.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HeathyMe_Entities : DbContext
    {
        public HeathyMe_Entities()
            : base("name=HeathyMe_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<DietPlan> DietPlans { get; set; }
        public virtual DbSet<Exercis> Exercises { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
    }
}