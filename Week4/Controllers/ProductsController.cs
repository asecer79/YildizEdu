using Microsoft.AspNetCore.Mvc;
using Week4.Data;
using Week4.Models.Entities;

namespace Week4.Controllers
{
    public class ProductsController : Controller
    {
        private MyDbContext _myDbContext;

        public ProductsController(MyDbContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public IActionResult Index()
        {
            var products = _myDbContext.Products.ToList(); //Select * from Products

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var product = new Product();

            return View(product);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _myDbContext.Products.Add(product);  //Insert Into Products (Name, Price, Category) values(product.name, ....)

                _myDbContext.SaveChanges(); //commit transaction

                return RedirectToAction("Index", "Products");
            }

            return View(product);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _myDbContext.Products.FirstOrDefault(p => p.Id == id); // Select * From Products Where Id = id

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var old = _myDbContext.Products.FirstOrDefault(p => p.Id == product.Id); // Select * From Products Where Id = id

                old.Id = product.Id;
                old.Name = product.Name;
                old.Price = product.Price;
                old.Category = product.Category;

                _myDbContext.Products.Update(old);  //Update Products Set Name = product.NAme .... where Id = product.id

                _myDbContext.SaveChanges(); //commit transaction

                return RedirectToAction("Index", "Products");
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _myDbContext.Products.FirstOrDefault(p => p.Id == id); // Select * From Products Where Id = id

            _myDbContext.Products.Remove(product); //Delete from Products where Id= id

            _myDbContext.SaveChanges(); //commit transaction

            return RedirectToAction("Index", "Products");
        }

    }
}
