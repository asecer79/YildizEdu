using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yildiz.Edu.Entities.Concrete;

public class LibraryLoan
{
    [Key]
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get; set; } = DateTime.Now;
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal FineAmount { get; set; } = 0.00m;
    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }
    [ForeignKey("BookId")]
    public virtual LibraryBook Book { get; set; }
}