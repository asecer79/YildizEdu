using Core.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yildiz.Edu.Entities.Concrete;

public class Course : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(10)]
    public string CourseCode { get; set; }

    [Required, MaxLength(100)]
    public string CourseName { get; set; }

    public int Credits { get; set; }

    public int DepartmentId { get; set; }
    public int InstructorId { get; set; }
    public int SemesterId { get; set; }

    public int MaxCapacity { get; set; } = 50;

    [ForeignKey("DepartmentId")]
    public virtual Department Department { get; set; }

    [ForeignKey("InstructorId")]
    public virtual Instructor Instructor { get; set; }

    [ForeignKey("SemesterId")]
    public virtual Semester Semester { get; set; }
}