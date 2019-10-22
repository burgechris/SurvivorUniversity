using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace University.Models
{
    public class UniversityContext : IdentityDbContext<ApplicationUser> 
    {
        public virtual DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }

        public UniversityContext(DbContextOptions options) : base(options) { }
    }
}