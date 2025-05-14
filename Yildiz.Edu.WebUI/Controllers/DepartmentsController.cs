using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Yildiz.Edu.Business.Abstract;
using Yildiz.Edu.Business.Concrete;
using Yildiz.Edu.Entities.Concrete;

namespace Yildiz.Edu.WebUI.Controllers
{
    public class DepartmentsController : Controller
    {
        private HttpClient client;

        public DepartmentsController()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7136")

            };
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await client.GetAsync("api/Departments/GetList");

            var data = await response.Content.ReadAsStringAsync();
            var faculties = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Department>>(data);
            return await Task.FromResult<IActionResult>(View(faculties));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var response = await client.GetAsync($"api/Departments/Details/{id}");
            var data = await response.Content.ReadAsStringAsync();

            var department =Newtonsoft.Json.JsonConvert.DeserializeObject<Department>(data);

            return View(department);
        }

        // GET: Departments/Create
        public async Task<IActionResult> Create()
        {
            var response = await client.GetAsync($"api/Faculties/GetList");
            var data = await response.Content.ReadAsStringAsync();

            var faculties = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Faculty>>(data);

            ViewData["FacultyId"] = new SelectList(faculties, "Id", "FacultyName");

            return View( new Department());
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentName,FacultyId,HeadOfDepartment")] Department department)
        {
            if (ModelState.IsValid)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(department);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Departments/Create", content);

                return RedirectToAction("Index", "Departments");

            }


            var response2 = await client.GetAsync($"api/Faculties/GetList");
            var data = await response2.Content.ReadAsStringAsync();

            var faculties = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Faculty>>(data);

            ViewData["FacultyId"] = new SelectList(faculties, "Id", "FacultyName");

            return await Task.FromResult<IActionResult>(View(department));
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var response = await client.GetAsync($"api/Departments/Details/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var department = Newtonsoft.Json.JsonConvert.DeserializeObject<Department>(content);


            var response2 = await client.GetAsync($"api/Faculties/GetList");
            var data = await response2.Content.ReadAsStringAsync();

            var faculties = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Faculty>>(data);

            ViewData["FacultyId"] = new SelectList(faculties, "Id", "FacultyName");

            return await Task.FromResult<IActionResult>(View(department));
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentName,FacultyId,HeadOfDepartment")] Department department)
        {

            if (ModelState.IsValid)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(department);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Departments/Edit", content);

                return RedirectToAction("Index", "Departments");

            }


            var response2 = await client.GetAsync($"api/Faculties/GetList");
            var data = await response2.Content.ReadAsStringAsync();

            var faculties = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Faculty>>(data);

            ViewData["FacultyId"] = new SelectList(faculties, "Id", "FacultyName");

            return await Task.FromResult<IActionResult>(View(department));
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var response = await client.GetAsync($"api/Departments/Details/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var department = Newtonsoft.Json.JsonConvert.DeserializeObject<Department>(content);


            return await Task.FromResult<IActionResult>(View(department));
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await client.GetAsync($"api/Departments/Details/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var department = Newtonsoft.Json.JsonConvert.DeserializeObject<Department>(content);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(department);

            var content2 = new StringContent(json, Encoding.UTF8, "application/json");

            var response2 = await client.PostAsync("api/Departments/Delete", content2);


            return RedirectToAction("Index", "Departments");

        }


    }
}
