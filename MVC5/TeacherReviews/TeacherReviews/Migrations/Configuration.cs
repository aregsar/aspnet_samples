namespace TeacherReviews.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TeacherReviews.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TeacherReviews.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TeacherReviews.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

              //You can use the DbSet<T>.AddOrUpdate() helper extension method 
              //to avoid creating duplicate seed data. E.g.

            //context.Teachers.AddOrUpdate(
            //      p => p.Name,
            //      new Teacher { Name = "Martin Fowler" },
            //      new Teacher { Name = "Bertrand Meyer" },
            //      new Teacher { Name = "Guido van Rossum" },
            //      new Teacher { Name = "Douglas Crockford" },
            //      new Teacher { Name = "Andres Hejlsberg" },
            //      new Teacher { Name = "Bob Martin" }
            //    );
            

           
        }
    }
}
