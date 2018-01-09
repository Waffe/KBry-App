using System.Collections.Generic;
using Kbry.Data.Model;

namespace Kbry.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Kbry.Data.KbryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Kbry.Data.KbryDbContext context)
        {
            var klass = new Class(){Name = "NET16"};
            var student1 = new Student() { Class = klass, Email = "kenisfenis@gmail.com", FirstName = "Kenan", LastName = "Alic", RegistrationCode = "445566" };
            var student2 = new Student() { Class = klass, Email = "niclasv@gmail.com", FirstName = "Niclas", LastName = "Valfridsson", RegistrationCode = "112233" };
            context.Classes.Add(klass);
            context.Students.Add(student2);
            context.Students.Add(student1);

            context.Attendances.AddRange(new List<Attendance>()
            {
                new Attendance() { Student = student2, Date = DateTime.Parse("2017/10/20 08:37:58")},
                new Attendance() {Student = student2, Date = DateTime.Parse("2017/10/21 08:27:58") },
                new Attendance() {Student = student2, Date = DateTime.Parse("2017/10/22 08:37:58") },
                new Attendance() {Student = student2, Date = DateTime.Parse("2017/10/23 08:30:58") },
                new Attendance() {Student = student2, Date = DateTime.Parse("2017/10/24 08:37:58") },
            });

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
