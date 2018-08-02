using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoedingsstoffenCalcu.DomainClasses;

namespace VoedingsstoffenCalcu.DomainModel
{
    public class VoedingsstoffenContext :DbContext
    {
        public VoedingsstoffenContext() :base ("name=VoedingsstoffenDb")
        {
            //"VoedingsstoffenDb")
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<DayEntry> DayEntrys { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<SavedProduct> SavedProducts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
                //"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\VoedingsstoffenDb.mdf;Integrated Security=True;Connect Timeout=30";
                 modelBuilder.Entity<DayEntry>()
                .HasRequired(s => s.Result)
                .WithRequiredPrincipal(ad => ad.DayEntry);

            base.OnModelCreating(modelBuilder);
        }
    }
}
