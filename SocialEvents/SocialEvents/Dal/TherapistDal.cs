using SocialEvents.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SocialEvents.Dal
{
    public class TherapistDal: DbContext
    {
        public DbSet<Therapist> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Therapist>().ToTable("Therapist");
        }

    }
}