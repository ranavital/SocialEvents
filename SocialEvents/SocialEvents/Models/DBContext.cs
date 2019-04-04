using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace SocialEvents.Models
{
    public class DBContext : DbContext
    {
        public DBContext() : base("Name=SocialEvents")
        {
            Database.SetInitializer<DBContext>(new DropCreateDatabaseIfModelChanges<DBContext>());
        }
        public DbSet<Situation> Situations { get; set; }
    }
}