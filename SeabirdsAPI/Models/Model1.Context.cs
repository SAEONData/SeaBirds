﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SeabirdsAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SEABIRDSEntities2 : DbContext
    {
        public SEABIRDSEntities2()
            : base("name=SEABIRDSEntities2")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cruise> Cruises { get; set; }
        public virtual DbSet<Observation> Observations { get; set; }
        public virtual DbSet<Observer> Observers { get; set; }
        public virtual DbSet<Shipboard> Shipboards { get; set; }
        public virtual DbSet<Species> Species { get; set; }
        public virtual DbSet<Transect> Transects { get; set; }
        public virtual DbSet<Vessel> Vessels { get; set; }
    }
}
