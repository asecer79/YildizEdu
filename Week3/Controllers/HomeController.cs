using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Week3.Controllers
{


    //[Route("Anasayfa")]
    //[Route("Home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        //[Route("Sayfa1")]
        //[Route("Index/{id?}")]
       // [HttpGet("Page1")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Hakkinda")]
        public IActionResult Privacy()
        {
            return View();
        }

   
    }
}
