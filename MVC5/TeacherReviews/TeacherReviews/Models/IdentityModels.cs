using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Collections.Generic;


namespace TeacherReviews.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public string Email { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherReview> TeacherReviews { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            
        }
    }


    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<TeacherReview> Reviews { get; set; }
    }

    public class TeacherReview
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string Review { get; set; }
        public string ReviewerName { get; set; }
    }
}