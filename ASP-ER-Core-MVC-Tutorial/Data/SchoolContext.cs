using ASP_ER_Core_MVC_Tutorial.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP_ER_Core_MVC_Tutorial.Data;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }
    
    // In this context, the DbSet properties represent the entity sets that correspond to tables in the database.
    // The names of the DbSet properties are used as table names in the database.
    // An entity set typically corresponds to a database table, and an entity corresponds to a row in the table.
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Student> Students { get; set; }

    // By default, the database will be created with tables that have the same name as the DbSet properties.
    // Here we override the OnModelCreating method to specify singular table names.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().ToTable("Course");
        modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
        modelBuilder.Entity<Student>().ToTable("Student");
    }
}