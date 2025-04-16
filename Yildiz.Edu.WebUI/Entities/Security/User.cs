using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace Yildiz.Edu.WebUI.Entities.Security
{
    public class User
    {
        [Key] public int Id { get; set; }

        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        [Required] public string Email { get; set; }

        [Required] public string Password { get; set; }
        
        public ICollection<OperationClaim>? OperationClaims { get; set; }
    }
}