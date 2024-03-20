using ASP_ER_Core_MVC_Tutorial.Models;

namespace ASP_ER_Core_MVC_Tutorial.Data;

public static class DbInitializer
{
    public static void Initialize(SchoolContext context)
    {
        // EnsureCreated is used to create the database if it doesn't exist.
        context.Database.EnsureCreated();

        // Skip populating the DB if it's already been seeded
        if (context.Students.Any())
        {
            return;   // DB has been seeded
        }

        var students = new Student[]
        {
            new() { FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01") },
            new() { FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01") },
            new() { FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01") },
            new() { FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01") },
            new() { FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01") },
            new() { FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01") },
            new() { FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01") },
            new() { FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01") }
        };
        
        foreach (var s in students)
        {
            context.Students.Add(s);
        }
        
        // Hits the database and saves the changes
        context.SaveChanges();

        var courses = new Course[]
        {
            new() { CourseID=1050,Title="Chemistry",Credits=3 },
            new() { CourseID=4022,Title="Microeconomics",Credits=3 },
            new() { CourseID=4041,Title="Macroeconomics",Credits=3 },
            new() { CourseID=1045,Title="Calculus",Credits=4 },
            new() { CourseID=3141,Title="Trigonometry",Credits=4 },
            new() { CourseID=2021,Title="Composition",Credits=3 },
            new() { CourseID=2042,Title="Literature",Credits=4 }
        };
        
        foreach (var c in courses)
        {
            context.Courses.Add(c);
        }
        
        context.SaveChanges();

        var enrollments = new Enrollment[]
        {
            new() { StudentID=1,CourseID=1050,Grade=Grade.A },
            new() { StudentID=1,CourseID=4022,Grade=Grade.C },
            new() { StudentID=1,CourseID=4041,Grade=Grade.B },
            new() { StudentID=2,CourseID=1045,Grade=Grade.B },
            new() { StudentID=2,CourseID=3141,Grade=Grade.F },
            new() { StudentID=2,CourseID=2021,Grade=Grade.F },
            new() { StudentID=3,CourseID=1050 },
            new() { StudentID=4,CourseID=1050 },
            new() { StudentID=4,CourseID=4022,Grade=Grade.F },
            new() { StudentID=5,CourseID=4041,Grade=Grade.C },
            new() { StudentID=6,CourseID=1045 },
            new() { StudentID=7,CourseID=3141,Grade=Grade.A }
        };
        
        foreach (var e in enrollments)
        {
            context.Enrollments.Add(e);
        }
        
        context.SaveChanges();
    }
}