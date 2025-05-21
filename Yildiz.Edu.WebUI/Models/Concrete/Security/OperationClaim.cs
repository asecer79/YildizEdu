using System.ComponentModel.DataAnnotations;

namespace Yildiz.Edu.WebUI.Models.Concrete.Security;

public class OperationClaim
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; }

    public ICollection<User>? Users { get; set; } = new List<User>();
}