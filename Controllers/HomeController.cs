using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    

    private readonly ILogger<HomeController> _logger;
        private MyContext dbContext;
    
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context,ILogger<HomeController> logger)
        {
            _logger = logger;
            dbContext = context;
        }
    
        public IActionResult Index()
        {
            List<Dish> AllDishes = dbContext.Dishes
                .OrderByDescending(d => d.CreatedAt)
                .ToList();

            ViewBag.AllDishes = AllDishes;
            
            return View();
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("AddNewDish")]
        public IActionResult AddNewDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                if (dbContext.Dishes.Any(d => d.Name == newDish.Name))
                {
                    ModelState.AddModelError("Name", "A Dish with this name already exists!");
                    return View("Create");
                }
                else
                {
                    dbContext.Add(newDish);
                    dbContext.SaveChanges();
                    Console.WriteLine($"\nNew dish added to DB: {newDish.Name}\n");
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View("Create");
            }
        }
        [HttpPost("UpdateDish")]
        public IActionResult UpdateDish(Dish updatedDish)
        {

            Dish actualDish = dbContext.Dishes.FirstOrDefault(d => d.Id == updatedDish.Id);

            if(ModelState.IsValid)
            {
                actualDish.Chef = updatedDish.Chef;
                actualDish.Calories = updatedDish.Calories;
                actualDish.Tastiness = updatedDish.Tastiness;
                actualDish.Description = updatedDish.Description;
                actualDish.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                Console.WriteLine($"\nDish updated in DB: {updatedDish.Name}\n");
                return RedirectToAction("Index");
            }
            else
            {
                return View("dish/edit/{actualDish.Id}", updatedDish);
            }
        }

        [HttpGet("dish/{id}")]
        public IActionResult ViewDish(int id)
        {
            Dish thisDish = dbContext.Dishes
                .FirstOrDefault(d => d.Id == id);
            ViewBag.ThisDish = thisDish;
            return View();
        }

        [HttpGet("dish/edit/{id}")]
        public IActionResult EditDish(int id)
        {
            Dish thisDish = dbContext.Dishes
                .FirstOrDefault(d => d.Id == id);
            ViewBag.ThisDish = thisDish;
            return View();
        }  

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
