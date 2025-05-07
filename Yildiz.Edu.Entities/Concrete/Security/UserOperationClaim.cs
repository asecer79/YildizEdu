using Core.DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yildiz.Edu.Entities.Concrete.Security;

public class UserOperationClaim : IEntity
{
    [Key] 
    public int Id { get; set; }

    [Required]
    [ForeignKey("UserId")]
    public int UserId { get; set; }

    [Required]
    [ForeignKey("OperationClaimId")]
    public int OperationClaimId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    [ForeignKey("OperationClaimId")]
    public OperationClaim? OperationClaim { get; set; }
        
}