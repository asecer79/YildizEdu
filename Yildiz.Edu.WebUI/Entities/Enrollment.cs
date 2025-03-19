using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yildiz.Edu.WebUI.Entities;

public class Enrollment
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public int SemesterId { get; set; }

    public DateTime EnrollmentDate { get; set; } = DateTime.Now;

    public decimal? Grade { get; set; }
    public decimal? AttendancePercentage { get; set; }

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }

    [ForeignKey("CourseId")]
    public virtual Course Course { get; set; }

    [ForeignKey("SemesterId")]
    public virtual Semester Semester { get; set; }
}