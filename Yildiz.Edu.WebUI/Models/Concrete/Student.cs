using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yildiz.Edu.WebUI.Models.Concrete;

public class Student 
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(10)]
    public string StudentNumber { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; }

    [Required, MaxLength(50)]
    public string LastName { get; set; }

    public DateTime? BirthDate { get; set; }

    [MaxLength(1)]
    public string Gender { get; set; }

    [Required, MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(15)]
    public string Phone { get; set; }

    public string Address { get; set; }

    public int DepartmentId { get; set; }

    public DateTime RegistrationDate { get; set; } = DateTime.Now;

    [MaxLength(20)]
    public string Status { get; set; } = "Active";

    [ForeignKey("DepartmentId")]
    public virtual Department Department { get; set; }
}