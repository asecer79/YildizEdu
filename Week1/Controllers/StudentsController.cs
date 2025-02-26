using Microsoft.AspNetCore.Mvc;
using Week1.Db;
using Week1.Models;

namespace Week1.Controllers
{
    //CRUD

    public class StudentsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var students = SchoolDb.Students.OrderBy(p=>p.Id).ToList();

           // ViewBag.students = students;

            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var student = new Student();

            return View(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {  
            
         

            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                ViewBag.errors = errors;

                return View(student);



            }

          

            SchoolDb.Students.Add(student);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = SchoolDb.Students.FirstOrDefault(p => p.Id == id);

            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {

            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                ViewBag.errors = errors;

                return View(student);



            }

            var oldStudent = SchoolDb.Students.FirstOrDefault(p => p.Id == student.Id);
            SchoolDb.Students.Remove(oldStudent);

           

            SchoolDb.Students.Add(student);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var oldStudent = SchoolDb.Students.FirstOrDefault(p => p.Id == id);
            SchoolDb.Students.Remove(oldStudent);

            return RedirectToAction("Index");
        }
    }
}
