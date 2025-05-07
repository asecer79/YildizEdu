using Core.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yildiz.Edu.Entities.Concrete;

public class Department : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string DepartmentName { get; set; }

    public int FacultyId { get; set; }

    [MaxLength(100)]
    public string HeadOfDepartment { get; set; }

    [ForeignKey("FacultyId")]
    public virtual Faculty? Faculty { get; set; }

    public virtual ICollection<Student>? Students { get; set; }
    public virtual ICollection<Instructor>? Instructors { get; set; }
    public virtual ICollection<Course>? Courses { get; set; }
}