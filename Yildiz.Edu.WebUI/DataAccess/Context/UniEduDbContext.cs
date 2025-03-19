using Microsoft.EntityFrameworkCore;
using Yildiz.Edu.WebUI.Entities;

namespace Yildiz.Edu.WebUI.DataAccess.Context
{
    public class UniEduDbContext() : DbContext()
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=UniEduDb;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;");
            }
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DisciplinaryRecord> DisciplinaryRecords { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<LibraryBook> LibraryBooks { get; set; }
        public DbSet<LibraryLoan> LibraryLoans { get; set; }
        public DbSet<Semester> Payments { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Student> Students { get; set; }
   
    }
}
