using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Yildiz.Edu.Entities.Concrete;

namespace Yildiz.Edu.WebUI.Controllers
{
    [Authorize]
    public class FacultiesController : Controller
    {

        private HttpClient client;

        public FacultiesController()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7136")
            };
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await client.GetAsync("api/Faculties/GetList");

            var data = await response.Content.ReadAsStringAsync();
            var faculties = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Faculty>>(data);
            return await Task.FromResult<IActionResult>(View(faculties));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var response = await client.GetAsync($"api/Faculties/Details/{id}");
            var data = await response.Content.ReadAsStringAsync();

            var faculty = Newtonsoft.Json.JsonConvert.DeserializeObject<Faculty>(data);

            return View(faculty);
        }

        // GET: Faculties/Create
        public async Task<IActionResult> Create()
        {
            var response = await client.GetAsync($"api/Faculties/GetList");
            var data = await response.Content.ReadAsStringAsync();

            return View(new Faculty());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(faculty);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Faculties/Create", content);

                return RedirectToAction("Index", "Faculties");

            }

            return await Task.FromResult<IActionResult>(View(faculty));
        }

        // GET: Faculties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var response = await client.GetAsync($"api/Faculties/Details/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var faculty = Newtonsoft.Json.JsonConvert.DeserializeObject<Faculty>(content);

            return await Task.FromResult<IActionResult>(View(faculty));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FacultyName,FacultyId,HeadOfFaculty")] Faculty faculty)
        {

            if (ModelState.IsValid)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(faculty);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/Faculties/Edit", content);

                return RedirectToAction("Index", "Faculties");

            }


            return await Task.FromResult<IActionResult>(View(faculty));
        }

        // GET: Faculties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var response = await client.GetAsync($"api/Faculties/Details/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var faculty = Newtonsoft.Json.JsonConvert.DeserializeObject<Faculty>(content);


            return await Task.FromResult<IActionResult>(View(faculty));
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await client.GetAsync($"api/Faculties/Details/{id}");
            var content = await response.Content.ReadAsStringAsync();

            var faculty = Newtonsoft.Json.JsonConvert.DeserializeObject<Faculty>(content);

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(faculty);

            var content2 = new StringContent(json, Encoding.UTF8, "application/json");

            var response2 = await client.PostAsync("api/Faculties/Delete", content2);


            return RedirectToAction("Index", "Faculties");

        }
    }
}
