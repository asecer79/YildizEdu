using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Diagnostics;
using Yildiz.Edu.DataAccess.Dal.Abstract;
using Yildiz.Edu.Entities.Concrete;

namespace Yildiz.Edu.WebUI.Controllers
{
    [Authorize]
    public class FacultiesController(IFacultyDal facultyDal, IMemoryCache memoryCache, IDistributedCache distributedCache) : Controller
    {
        //RedisManagerV1 redisManager = new RedisManagerV1();

        IDistributedCache distributedCache = distributedCache;

        //[Authorize]
        public async Task<IActionResult> Index()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var key = "facultyList";

            // var cachedData = memoryCache.Get<List<Faculty>>(key);
            //  var cachedData = redisManager.Get<List<Faculty>>(key);
            IList<Faculty> cachedData = null;
            var jsonString = distributedCache.GetString(key);

            if (jsonString!=null)
            {
                cachedData =JsonConvert.DeserializeObject<List<Faculty>>(jsonString);
            }

            if (cachedData != null)
            {

                ViewBag.ElapsedMs = sw.Elapsed.TotalMilliseconds;

                return await Task.Run(() => View(cachedData));
            }
            else
            {
                await Task.Delay(3000);//artificial delay for only test purpose

                var liveData = facultyDal.GetAll();

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(60),
                    
                };

                // memoryCache.Set(key, liveData, options);
               // redisManager.Set(key, liveData);
               distributedCache.SetString(key,JsonConvert.SerializeObject(liveData));

                ViewBag.ElapsedMs = sw.Elapsed.TotalMilliseconds;
                
                return await Task.Run(() => View(liveData));
            }
        }

        // GET: Faculties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = facultyDal.GetAll().FirstOrDefault(m => m.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return await Task.Run(() => View(faculty));
        }

        // GET: Faculties/Create
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Faculties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Create(Faculty faculty)
        {
            if (ModelState.IsValid)
            {
                facultyDal.Add(faculty);

                var key = "facultyList";
                //memoryCache.Remove(key);
                //redisManager.Remove(key);
                distributedCache.Remove(key);

                return RedirectToAction(nameof(Index));
            }
            return await Task.Run(() => View(faculty));
        }

        // GET: Faculties/Edit/5
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Edit(int id)
        {

            var faculty = facultyDal.Get(id);
            return await Task.Run(() => View(faculty));
        }

        // POST: Faculties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FacultyName,DeanName,EstablishedDate")] Faculty faculty)
        {
            if (id != faculty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    facultyDal.Update(faculty);

                    var key = "facultyList";
                    //memoryCache.Remove(key);
                    //redisManager.Remove(key);
                    distributedCache.Remove(key);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyExists(faculty.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return await Task.Run(() => View(faculty));
        }

        // GET: Faculties/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = facultyDal.GetAll()
                .FirstOrDefault(m => m.Id == id);
            if (faculty == null)
            {
                return NotFound();
            }

            return await Task.Run(() => View(faculty));
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            facultyDal.Delete(id);

            var key = "facultyList";
            //memoryCache.Remove(key);
            //redisManager.Remove(key);
            distributedCache.Remove(key);


            return RedirectToAction(nameof(Index));
        }

        private bool FacultyExists(int id)
        {
            return facultyDal.Get(id) != null!;
        }
    }
}
