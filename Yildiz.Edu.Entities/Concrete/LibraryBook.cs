using Core.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yildiz.Edu.Entities.Concrete;

public class LibraryBook : IEntity
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(13)]
    public string ISBN { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; }

    [MaxLength(100)]
    public string Author { get; set; }

    public int? PublicationYear { get; set; }

    public int DepartmentId { get; set; }

    public int AvailableCopies { get; set; } = 1;

    [ForeignKey("DepartmentId")]
    public virtual Department Department { get; set; }
}