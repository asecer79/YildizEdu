using System.ComponentModel.DataAnnotations;

namespace Core.DataAccess.Entities
{
    public interface IEntity
    {
        [Key]
        public int Id { get; set; }


    }
}
