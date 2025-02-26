using Microsoft.AspNetCore.Mvc;

namespace Week2.Controllers
{
    public class HomeController : Controller
    {
     

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
