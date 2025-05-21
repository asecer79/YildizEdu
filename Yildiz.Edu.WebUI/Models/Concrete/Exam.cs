using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yildiz.Edu.WebUI.Models.Concrete;

public class Exam 
{
    [Key]
    public int Id { get; set; }

    public int CourseId { get; set; }

    public DateTime ExamDate { get; set; }

    [MaxLength(20)]
    public string ExamType { get; set; } // Midterm, Final, Makeup, Quiz

    public decimal MaxScore { get; set; } = 100.00m;

    [MaxLength(20)]
    public string Classroom { get; set; }

    public int? DurationMinutes { get; set; }

    [ForeignKey("CourseId")]
    public virtual Course Course { get; set; }
}