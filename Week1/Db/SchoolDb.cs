using Week1.Models;

namespace Week1.Db
{
    public class SchoolDb
    {
        
        public static List<Student>? Students { get; set; } = new List<Student>()
        {

            new Student()
            {
                Id = 1,
                Name = "Ayşe",
                Description = "d1",
                StudentId = "1"
            },
            new Student()
            {
                Id = 2,
                Name = "Mehmet",
                Description = "d2",
                StudentId = "2"
            },
            new Student()
            {
                Id = 3,
                Name = "Mustafa",
                Description = "d3",
                StudentId = "3"
            },
            new Student()
            {
                Id = 4,
                Name = "Melih",
                Description = "d4",
                StudentId = "4"
            },
            new Student()
            {
                Id = 5,
                Name = "İsmail",
                Description = "d5",
                StudentId = "5"
            },

        };
    }
}
