using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Project_Gym_Asp_Net.Models;
using Project_Gym_Asp_Net.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_Gym_Asp_Net.Controllers
{
    public class ScheduleController : Controller
    {
        UserManager<User> userManager;
        MyContext context;
        public ScheduleController(MyContext cont, UserManager<User> manager)
        {
            context = cont;
            userManager = manager;
        }

        public IActionResult AllSchedule()
        {
            return View(context.Schedules.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> CreateSchedule(string id)
        {
            User user = await userManager.FindByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            ViewData["Days"] = new SelectList(new List<string>(){"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"});
            ViewData["UserId"] = user.Id;
            ViewData["Exercises"] = context.Exercises.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(Schedule schedule, string userId, int[] selectedExercises)
        {
            if (ModelState.IsValid)
            {
                Schedule schedule1 = new Schedule();
                schedule1.Day = schedule.Day;
                schedule1.User = await userManager.FindByIdAsync(userId);
                if (selectedExercises != null)
                {
                    foreach (var c in context.Exercises.Where(c => selectedExercises.Contains(c.Id)))
                    {
                        schedule1.Exercises.Add(c);
                    }
                }                
                context.Schedules.Add(schedule1);
                context.SaveChanges();
                return RedirectToAction("AllUsers", "Admin");
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditSchedule(int? id)
        {
            Schedule schedule = context.Schedules.Where(s => s.Id == id).FirstOrDefault();
            if (schedule == null)
            {
                return NotFound();
            }                      
            ViewBag.Exercises = context.Exercises.ToList();
            return View(schedule);
        }

        [HttpPost]
        public async Task<IActionResult> EditSchedule(Schedule schedule, string userId, int[] selectedExercises)
        {
            if (ModelState.IsValid)
            {
                Schedule schedule1 = context.Schedules.FirstOrDefault(s => s.Id == schedule.Id);
                schedule1.Day = schedule.Day;
                schedule1.User = await userManager.FindByIdAsync(userId);
                schedule1.Exercises.Clear();
                if(selectedExercises != null)
                {
                    foreach(var c in context.Exercises.Where(c => selectedExercises.Contains(c.Id)))
                    {
                        schedule1.Exercises.Add(c);
                    }
                }              
                
                context.Schedules.Update(schedule1);
                context.SaveChanges();
                return RedirectToAction("AllUsers", "Admin");
            }
            return View();
        }

        [HttpGet]
        public IActionResult DeleteSchedule(int? id)
        {
            Schedule sch = context.Schedules.Find(id);
            if(sch == null)
            {
                return NotFound();
            }
            return View(sch);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSchedule(Schedule schedule)
        {
            context.Schedules.Remove(schedule);
            context.SaveChanges();
            return RedirectToAction("AllUsers", "Admin");
        }

    }
}
