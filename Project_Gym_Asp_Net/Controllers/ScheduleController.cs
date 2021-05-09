using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project_Gym_Asp_Net.Models;
using Project_Gym_Asp_Net.ViewModels;

namespace Project_Gym_Asp_Net.Controllers
{
    public class ScheduleController : Controller
    {
        MyContext context;
        public ScheduleController(MyContext cont)
        {
            context = cont;
        }

        [HttpGet]
        public IActionResult AddSchedule(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            ViewBag.UserId = id;
            return View();
        }

        [HttpPost]
        public IActionResult AddSchedule(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                Schedule sched = new Schedule();
                sched.Day = schedule.Day;
                sched.User = context.Users.Select(u => u).Where(u => u.Id == schedule.User.Id).FirstOrDefault();
                sched.Categories = context.Categories.ToList();

            }
            return RedirectToAction("AllSchedule", "Schedule");
        }

        public IActionResult AllSchedule()
        {
            return View(context.Schedules.ToList());
        }
    }
}
