using Core.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yildiz.Edu.Entities.Concrete;

public class Instructor : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string FirstName { get; set; }

    [Required, MaxLength(50)]
    public string LastName { get; set; }

    [Required, MaxLength(100)]
    public string Email { get; set; }

    [MaxLength(15)]
    public string Phone { get; set; }

    public int DepartmentId { get; set; }

    [MaxLength(50)]
    public string Title { get; set; }

    public DateTime? HireDate { get; set; }

    [ForeignKey("DepartmentId")]
    public virtual Department Department { get; set; }
}