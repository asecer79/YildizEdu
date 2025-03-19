using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yildiz.Edu.WebUI.Entities;

public class DisciplinaryRecord
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    public DateTime IncidentDate { get; set; }
    public string Description { get; set; }
    public string Penalty { get; set; }
    public DateTime? DecisionDate { get; set; }

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; }
}