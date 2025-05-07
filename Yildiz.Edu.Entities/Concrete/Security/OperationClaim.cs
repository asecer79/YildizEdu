using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Core.DataAccess.Entities;

namespace Yildiz.Edu.Entities.Concrete.Security;

public class OperationClaim: IEntity
{
    [Key] public int Id { get; set; }
    [Required] public string Name { get; set; }

    public ICollection<User>? Users { get; set; } = new List<User>();
}