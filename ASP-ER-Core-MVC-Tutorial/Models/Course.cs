using System.ComponentModel.DataAnnotations.Schema;

namespace ASP_ER_Core_MVC_Tutorial.Models;

public class Course
{
    // This attribute is used to specify that the database should not generate the primary key value.
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CourseID { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
    
    public ICollection<Enrollment> Enrollments { get; set; }
}