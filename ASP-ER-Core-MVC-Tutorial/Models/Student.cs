namespace ASP_ER_Core_MVC_Tutorial.Models;

public class Student
{
    // Entity Framework interprets a property as a primary key if its name is ID or classnameID (StudentID in this case)
    public int ID { get; set; }
    public string LastName { get; set; }
    public string FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }
   
    // This is a collection navigation property. It holds other entities that are related to this entity.
    // Collection navigation props have to implement ICollection<T> interface. List<T> and HashSet<T> are common.
    // Arrays are invalid because they don't have an Add method.
    public ICollection<Enrollment> Enrollments { get; set; }
}