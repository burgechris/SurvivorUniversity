using System.Collections.Generic;

namespace University.Models
{
    public class Course
    {
        public Course()
        {
            this.Students = new HashSet<Enrollment>();
        }

        public int CourseId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Enrollment> Students { get; set; }
    }
}