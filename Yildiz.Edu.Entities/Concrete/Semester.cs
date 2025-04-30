using System.ComponentModel.DataAnnotations;

namespace Yildiz.Edu.Entities.Concrete;

public class Semester
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string SemesterName { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }
}