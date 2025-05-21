using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Diagnostics;
using Yildiz.Edu.Business.Abstract;
using Yildiz.Edu.Entities.Concrete;

namespace Yildiz.Edu.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FacultiesController : ControllerBase
    {
        //RedisManagerV1 redisManager = new RedisManagerV1();

        private readonly IFacultyService _facultyService;

        private readonly IDistributedCache _distributedCache;

        public FacultiesController(IDistributedCache distributedCache, IFacultyService facultyService)
        {
            this._distributedCache = distributedCache;
            _facultyService = facultyService;
        }

        //[Authorize]
        [HttpGet]
        [Route("GetList")]
        public async Task<IList<Faculty>> GetList()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var key = "facultyList";

            IList<Faculty> cachedData = null;
            var jsonString = _distributedCache.GetString(key);

            if (jsonString != null)
            {
                cachedData = JsonConvert.DeserializeObject<IList<Faculty>>(jsonString);
            }

            if (cachedData != null)
            {

                return await Task.Run(() => cachedData);
            }
            else
            {
                await Task.Delay(3000);//artificial delay for only test purpose

                var liveData = _facultyService.GetAll();

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
                {
                    SlidingExpiration = TimeSpan.FromSeconds(60),

                };

                _distributedCache.SetString(key, JsonConvert.SerializeObject(liveData));

                return await Task.Run(() => liveData.ToList());
            }
        }

        [HttpGet]
        [Route("Details/{id}")]
        public async Task<Faculty> Details(int? id)
        {
            if (id == null)
            {
                return new Faculty();
            }

            var faculty = _facultyService.GetAll().FirstOrDefault(m => m.Id == id);
            if (faculty == null)
            {
                return new Faculty();
            }

            return await Task.Run(() => faculty);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<Faculty> Create(Faculty faculty)
        {
            _facultyService.Add(faculty);

            var key = "facultyList";
            //memoryCache.Remove(key);
            //redisManager.Remove(key);
            _distributedCache.Remove(key);

            return await Task.Run(() => faculty);
        }



        [HttpPost]
        [Route("Edit")]
        public async Task<Faculty> Edit(Faculty faculty)
        {


            _facultyService.Update(faculty);

            var key = "facultyList";
            //memoryCache.Remove(key);
            //redisManager.Remove(key);
            _distributedCache.Remove(key);

            return await Task.Run(() =>faculty);
        }

   

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<bool> Delete(Faculty faculty)
        {

            _facultyService.Delete(faculty);

            var key = "facultyList";
            //memoryCache.Remove(key);
            //redisManager.Remove(key);
            _distributedCache.Remove(key);


            return await Task.Run(() => true);

        }

        private bool FacultyExists(int id)
        {
            return _facultyService.Get(p => p.Id == id) != null!;
        }

    }
}
