using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yildiz.Edu.Entities.Concrete;

public class Payment
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; } = DateTime.Now;

    public int SemesterId { get; set; }

    [MaxLength(20)]
    public string PaymentType { get; set; } // Tuition, Registration Fee, Additional Fee

    [MaxLength(20)]
    public string Status { get; set; } = "Paid"; // Paid, Pending, Cancelled

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }

    [ForeignKey("SemesterId")]
    public virtual Semester Semester { get; set; }
}