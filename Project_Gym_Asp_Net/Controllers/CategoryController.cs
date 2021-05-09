using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Gym_Asp_Net.Models;
using Project_Gym_Asp_Net.ViewModels;

namespace Project_Gym_Asp_Net.Controllers
{
    public class CategoryController : Controller
    {
        MyContext context;
        public CategoryController(MyContext myContext)
        {
            context = myContext;
        }

        public IActionResult AllCategory()
        {
            return View(context.Categories.ToList());
        }
        
        public IActionResult CategoryExfercise(int? id)
        {
            Category category = context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                //Category cat = new Category { CategoryName = category.CategoryName}

                // доробити логіку (перевірки IdentityResult і повернення назад.)
                context.Categories.Add(category);
                context.SaveChanges();
                return RedirectToAction("AllCategory", "Category");
            }
            return View();
        } 

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("AllCategory", "Category");
            }
            ViewBag.CategoryId = id;
            Category category = context.Categories.Where(c => c.Id == id).FirstOrDefault();
            return View(category);
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
            return RedirectToAction("AllCategory", "Category");
        }

        [HttpGet]
        public IActionResult DeleteCategory(int? id)
        {
            if(id == null)
            {
                NotFound();
            }
            Category category = context.Categories.Where(c => c.Id == id).FirstOrDefault();
            return View(category);
        }


        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Category category = context.Categories.Where(c => c.Id == id).FirstOrDefault();
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("AllCategory", "Category");
        }

        //[HttpPost]
        //public IActionResult DeleteCategory(Category category)
        //{
        //    context.Categories.Remove(category);
        //    context.SaveChanges();
        //    return RedirectToAction("AllCategory", "Category");
        //}
    }
}
