namespace University.Models
{
    public class Enrollment
    {
       public int EnrollmentId { get; set; }
       public int StudentId { get; set; }
       public int CourseId { get; set; }
       public string Date { get; set; }
       public Student Student { get; set; }
       public Course Course { get; set; } 
    }
}