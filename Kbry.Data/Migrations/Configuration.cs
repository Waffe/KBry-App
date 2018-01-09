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
            var fittklass = new Class(){Name = "NET16"};
            var fittstudent1 = new Student() { Class = fittklass, Email = "kenisfeenis@gmail.com", FirstName = "Kenan", LastName = "Alic", RegistrationCode = "445566" };
            var fittstudent2 = new Student() { Class = fittklass, Email = "niclasv@gmail.com", FirstName = "Niclas", LastName = "Valfridsson", RegistrationCode = "112233" };
            
            context.Classes.Add(fittklass);
            context.Students.Add(fittstudent2);
            context.Students.Add(fittstudent1);

            context.Attendances.AddRange(new List<Attendance>()
            {
                new Attendance() { Student = fittstudent2, Date = DateTime.Parse("2017/10/20 08:37:58")},
                new Attendance() {Student = fittstudent2, Date = DateTime.Parse("2017/10/21 08:27:58") },
                new Attendance() {Student = fittstudent2, Date = DateTime.Parse("2017/10/22 08:37:58") },
                new Attendance() {Student = fittstudent2, Date = DateTime.Parse("2017/10/23 08:30:58") },
                new Attendance() {Student = fittstudent2, Date = DateTime.Parse("2017/10/24 08:37:58") },
            });
            if (!context.ApiKeys.Any())
            {
                var myguid = Guid.Parse("9546482E-887A-4CAB-A403-AD9C326FFDA5");
                context.ApiKeys.Add(new ApiKey(){Key = myguid});
            }

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
