using BO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Module6TP1.Data
{
    public class Module6TP1Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Module6TP1Context() : base("name=Module6TP1Context")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samourai>().HasMany(s => s.ArtMartials).WithMany();
        }

        public System.Data.Entity.DbSet<BO.Samourai> Samourais { get; set; }

        public System.Data.Entity.DbSet<BO.Arme> Armes { get; set; }

        public System.Data.Entity.DbSet<BO.ArtMartial> ArtMartials { get; set; }
    }
}
