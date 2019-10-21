using Microsoft.EntityFrameworkCore;

namespace University.Models
{
    public class UniversityContext : DbContext 
    {
        public virtual DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }

        public UniversityContext(DbContextOptions options) : base(options) { }
    }
}