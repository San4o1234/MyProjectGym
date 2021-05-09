using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Gym_Asp_Net.Models;
using Project_Gym_Asp_Net.ViewModels;

namespace Project_Gym_Asp_Net.Controllers
{
    public class ExerciseController : Controller
    {
        MyContext context;
        public ExerciseController(MyContext cont)
        {
            context = cont;
        }

        public IActionResult AllExercise()
        {
            return View(context.Exercises.ToList());
        }
    
        [HttpGet]
        public IActionResult CreateExercise(int? id)
        {
            if(id == null)
            {
                return RedirectToAction("AllExercise", "Exercise");
            }
            ViewBag.CategoryId = id;
            return View();
        }
    
        [HttpPost]
        public IActionResult CreateExercise(Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                Exercise ex = new Exercise { ExerciseName = exercise.ExerciseName, CountApproach = exercise.CountApproach, Repeats = exercise.Repeats, Category = context.Categories.Where(c => c.Id == exercise.Category.Id).First()};
                context.Exercises.Add(ex);
                context.SaveChanges();
                return RedirectToAction("AllExercise", "Exercise");
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditExercise(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("AllExercise", "Exercise");
            }
            ViewBag.ExerciseId = id;
            Exercise exercise = context.Exercises.Where(e => e.Id == id).First();
            return View(exercise);
        }
        [HttpPost]
        public IActionResult EditExercise(Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                context.Exercises.Update(exercise);
                context.SaveChanges();
                return RedirectToAction("AllExercise", "Exercise");
            }
            return View(exercise);
        }

        [HttpGet]
        public IActionResult DeleteExercise(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            Exercise ex = context.Exercises.Find(id);
            if(ex == null)
            {
                return NotFound();
            }
            return View(ex);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteExercise(Exercise exercise)
        {
            context.Exercises.Remove(exercise);
            context.SaveChanges();
            return RedirectToAction("AllExercise", "Exercise");
        }
    }
}
