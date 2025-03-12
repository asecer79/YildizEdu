using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Week4.Models.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name değeri zorunludur")]
        [StringLength(15)]
        public string Name { get; set; }

        [Range(0,double.MaxValue,ErrorMessage = "Price cannot be negative")]
        public decimal Price { get; set; }

        [StringLength(15)]
        public string? Category { get; set; }
    }
}
