using Microsoft.AspNetCore.Mvc;
using Week1.Models;

namespace Week1.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            Student s1 = new Student()
            {
                Id = 1,
                Name = "Ahmet Karaca",
                Description = "Math Eng. Std.",
                StudentId = "13546516516546"

            };

            return View(s1);
        }
    }
}
