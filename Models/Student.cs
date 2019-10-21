using System.Collections.Generic;

namespace University.Models
{
    public class Student
    {
        public Student()
        {
            this.Courses = new HashSet<Enrollment>();
        }

        public int StudentId { get; set; }
        public string Name { get; set; }
        public bool Enrolled { get; set; }

        public ICollection<Enrollment> Courses { get; set; }
    }
}