using System.ComponentModel.DataAnnotations;

namespace Yildiz.Edu.Entities.Concrete;

public class Event
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string EventName { get; set; }

    public DateTime EventDate { get; set; }
}