using System.ComponentModel.DataAnnotations;

namespace Yildiz.Edu.WebUI.Entities
{
    // 1. Faculty Model
    public class Faculty
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FacultyName { get; set; }

        [MaxLength(100)]
        public string DeanName { get; set; }

        public DateTime? EstablishedDate { get; set; }

        public virtual ICollection<Department>? Departments { get; set; }
    }

    // 2. Department Model

    // 3. Student Model

    // 4. Instructor Model

    // 5. Semester Model

    // 6. Course Model

    // 7. Enrollment Model

    // 8. LibraryBook Model

    // 9. LibraryLoan Model

    // 10. Event Model

    // 11. DisciplinaryRecord Model
}
