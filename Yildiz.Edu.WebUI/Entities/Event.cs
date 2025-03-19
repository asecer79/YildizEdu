using System.ComponentModel.DataAnnotations;

namespace Yildiz.Edu.WebUI.Entities;

public class Event
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string EventName { get; set; }

    public DateTime EventDate { get; set; }
}