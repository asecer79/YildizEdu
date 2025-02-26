using System.ComponentModel.DataAnnotations;

namespace Week1.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Length(2,15)]
        public string? Name { get; set; }

        [Required]
        [Length(10,10)]
        public string? StudentId { get; set; }

        [MaxLength(50)]
        public string? Description { get; set; }
     
    }
}
