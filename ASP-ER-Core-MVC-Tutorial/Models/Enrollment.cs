namespace ASP_ER_Core_MVC_Tutorial.Models;

public enum Grade
{
    A, B, C, D, F
}

public class Enrollment
{
    // ID would be fine here
    public int EnrollmentID { get; set; }
    // CourseID and StudentID are foreign keys, with corresponding navigation properties Course and Student
    // These are reference navigation properties. They hold a single related entity.
    public int CourseID { get; set; }
    public int StudentID { get; set; }
    public Grade? Grade { get; set; }

    public Course Course { get; set; }
    public Student Student { get; set; }
}