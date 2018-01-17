using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kbry.Data.Model;

namespace Kbry.Data
{
    public class KbryDbContext : DbContext
    {
        public KbryDbContext() : base("KbryApiDB")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SchoolClass> Classes { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }

        public static KbryDbContext Create()
        {
            return new KbryDbContext();
        }

        //internal class EmployeeConfiguration : EntityTypeConfiguration<Student>
        //{
        //    public EmployeeConfiguration()
        //    {
        //        HasKey(f => f.Id);

        //        HasRequired(e => e.SchoolClass)
        //            .WithMany()
        //            .Map(c => c.MapKey("Class_Id"));

        //        ToTable("¨Students");
        //    }
        //}
    }
}
