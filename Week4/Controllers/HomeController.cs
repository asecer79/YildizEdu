using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Week4.Data;


namespace Week4.Controllers;

public class HomeController : Controller
{
   
    public IActionResult Index()
    {
      

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

  
}
