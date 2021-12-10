using Microsoft.EntityFrameworkCore;
using CRUDelicious.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Collections.Generic;
// Other using statements
namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            _context = context;
        }
     
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Dish> AllDishes = _context.Dishes.ToList();
            
            return View(AllDishes);
        }
        
        
        [HttpGet]
        [Route("new")]
        public IActionResult New()
        {
            return View("new");
        }
        [HttpGet]
        [Route("dish")]
        public IActionResult Dish()
        {
            return View("dish");
        }
        
        [HttpPost]
        [Route("create")]
        public IActionResult NewDish(Dish fromForm)
        {
            if(ModelState.IsValid)
            {
                _context.Add(fromForm);
                _context.SaveChanges();

                System.Console.WriteLine(fromForm.DishId);

                return RedirectToAction("DishInfo", new { dishId = fromForm.DishId});
                
            }
            else
                {
                    return View("new");
                }
        }
        [HttpGet]
        [Route("dish/info/{dishId}")]
        public IActionResult DishInfo(int dishId)
        {
            Dish toRender = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            return View(toRender);
        }

        [HttpGet]
        [Route("dish/delete/{dishId}")]
        public RedirectToActionResult DeleteDish(int dishId)
        {
            Dish toDelete = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

            _context.Dishes.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet("dish/edit/{dishId}")]
        public IActionResult EditDish(int dishId)
        {
            Dish toEdit = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
            if(toEdit == null)
            {
                return RedirectToAction("index");
            }
            
            return View("EditDish", toEdit);
        }
        [HttpPost("dish/update/{dishId}")]
        public IActionResult UpdateDish(int dishId, Dish fromForm)
        {
            if(ModelState.IsValid)
            {
                Dish inDb = _context.Dishes.FirstOrDefault(dish => dish.DishId == dishId);

                inDb.Name = fromForm.Name;
                inDb.Chef = fromForm.Chef;
                inDb.Calories = fromForm.Calories;
                inDb.Tastiness = fromForm.Tastiness;
                inDb.UpdatedAt = DateTime.Now;

                _context.SaveChanges();
                return RedirectToAction("DishInfo", new {dishId = dishId});
            }
            else
            {
                return View("EditDish", fromForm);
            }
        }
    }
 }
